using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : ControllerBase
    {
        private readonly autoresContext _autoresContexto;

        public autoresController(autoresContext autoresContexto)
        {
            _autoresContexto = autoresContexto;
        }

        //Método para leer todos los registros
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Autores> listadoAutores = (from e in _autoresContexto.autores
                                           select e).ToList();

            if (listadoAutores.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutores);
        }

        //Método para buscar por ID
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Autores autor = (from e in _autoresContexto.autores
                             where e.id_autores == id
                             select e).FirstOrDefault();

            if (autor == null)
            {
                return NotFound();

            }

            return Ok(autor);
        }

        //Método para crear o insertar registros
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarAutor([FromBody] Autores autor)
        {
            try
            {
                _autoresContexto.autores.Add(autor);
                _autoresContexto.SaveChanges();
                return Ok(autor);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los registros de la tabla
        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult ActualizarAutor(int id, [FromBody] Autores autorModificar)
        {
            Autores autorActual = (from e in _autoresContexto.autores
                                     where e.id_autores == id
                                     select e).FirstOrDefault();

            if (autorActual == null)
            { return NotFound(); }

            autorActual.nombre_autores = autorModificar.nombre_autores;


            _autoresContexto.Entry(autorActual).State = EntityState.Modified;
            _autoresContexto.SaveChanges();

            return Ok(autorModificar);

        }


        [HttpPut]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarAutores(int id)
        {
            Autores autores = (from e in _autoresContexto.autores
                               where e.id_autores == id
                               select e).FirstOrDefault();

            if (autores == null)
            { return NotFound(); }

            _autoresContexto.autores.Attach(autores);
            _autoresContexto.autores.Remove(autores);
            _autoresContexto.SaveChanges();

            return Ok(autores);

        }
    }
}
