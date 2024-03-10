using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresLibroController : ControllerBase
    {
        private readonly librosContext _autorLibroContext;

        public autoresLibroController(librosContext autorLibroContext)
        {
            _autorLibroContext = autorLibroContext;
        }

        /*Para obtener registros*/
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Autor_libro> listadoAutoresLibro = (from e in _autorLibroContext.autor_libro
                                                     select e).ToList();

            if (listadoAutoresLibro.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoAutoresLibro);
        }

        /*Método para buscar por ID*/
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Autor_libro? autor_libro = (from e in _autorLibroContext.autor_libro
                                        where e.id_autores == id  
                                        select e).FirstOrDefault();

            if (autor_libro == null)
            {
                return NotFound();

            }

            return Ok(autor_libro);
        }

        /*Busqueda de libros al ingresar el nombre del autor*/
        [HttpGet]
        [Route("Find/{nombre}")]

        public IActionResult Get(String nombre)
        {
            var listadoEquipo = (from e in _autorLibroContext.autor_libro
                                 join a in _autorLibroContext.autores
                                        on e.id_autores equals a.id_autores
                                 join l in _autorLibroContext.libros
                                        on e.id_libros equals l.id_libros
                                 where a.nombre_autores == nombre
                                 select new
                                 {
                                     e.id_autores,
                                     nombre = a.nombre_autores,
                                     libro = l.titulo_libros

                                 }).ToList();

            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipo);
        }

        /*Método para crear o insertar registros*/
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarAutorLibro([FromBody] Autor_libro autor_libro)
        {
            try
            {
                _autorLibroContext.autor_libro.Add(autor_libro);
                _autorLibroContext.SaveChanges();
                return Ok(autor_libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los registros de la tabla
        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult ActualizarAutorLibro(int id, [FromBody] Autor_libro autorLibroModificar)
        {
            Autor_libro? autorLibroActual = (from e in _autorLibroContext.autor_libro
                                    where e.id_autores == id
                                    select e).FirstOrDefault();

            if (autorLibroActual == null)
            { return NotFound(); }

            autorLibroActual.id_autores = autorLibroModificar.id_autores;
            autorLibroActual.id_libros = autorLibroModificar.id_libros;
            autorLibroModificar.orden = autorLibroModificar.orden;

            _autorLibroContext.Entry(autorLibroActual).State = EntityState.Modified;
            _autorLibroContext.SaveChanges();

            return Ok(autorLibroModificar);
        }

        /*Método para eliminar registros*/
        [HttpPut]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarAutorLibro(int id)
        {
            Autor_libro? autor_Libro = (from e in _autorLibroContext.autor_libro
                                        where e.id_autores == id
                                        select e).FirstOrDefault();

            if (autor_Libro == null)
            { return NotFound(); }

            _autorLibroContext.autor_libro.Attach(autor_Libro);
            _autorLibroContext.autor_libro.Remove(autor_Libro);
            _autorLibroContext.SaveChanges();

            return Ok(autor_Libro);

        }
    }
}
