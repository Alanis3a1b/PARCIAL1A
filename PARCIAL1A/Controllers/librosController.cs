using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librosController : ControllerBase
    {
        private readonly librosContext _librosContext;

        public librosController(librosContext librosContext)
        {
            _librosContext = librosContext;
        }

        //Método para leer todos los registros
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Libros> listadolibros = (from e in _librosContext.libros
                                            select e).ToList();

            if (listadolibros.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadolibros);
        }

        //Método para buscar por ID
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Libros? libro = (from e in _librosContext.libros
                             where e.id_libros == id
                             select e).FirstOrDefault();

            if (libro == null)
            {
                return NotFound();

            }

            return Ok(libro);
        }


        //Método para crear o insertar registros
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarLibros([FromBody] Libros libro)
        {
            try
            {
                _librosContext.libros.Add(libro);
                _librosContext.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los registros de la tabla
        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult ActualizarLibros(int id, [FromBody] Libros libroModificar)
        {
            Libros? libroActual = (from e in _librosContext.libros
                                  where e.id_libros == id
                                  select e).FirstOrDefault();

            if (libroActual == null)
            { return NotFound(); }

            libroActual.titulo_libros = libroModificar.titulo_libros;


            _librosContext.Entry(libroActual).State = EntityState.Modified;
            _librosContext.SaveChanges();

            return Ok(libroModificar);

        }
    }
}
