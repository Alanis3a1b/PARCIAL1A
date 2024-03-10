using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class postsController : ControllerBase
    {
        private readonly librosContext _librosContext;

        public postsController(librosContext librosContext)
        {
            _librosContext = librosContext;
        }

        /*Para obtener registros*/
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Posts> listadoPosts = (from e in _librosContext.posts
                                          select e).ToList();

            if (listadoPosts.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPosts);
        }

        /*Método para buscar por ID*/
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Posts? posts = (from e in _librosContext.posts
                              where e.id_posts == id
                              select e).FirstOrDefault();

            if (posts == null)
            {
                return NotFound();

            }

            return Ok(posts);
        }
         /*Método para obtener el listado de los últimos 20 posts al ingresar el nombre del autor*/
        [HttpGet]
        [Route("GetPosts")]

        public IActionResult Get(string AutorNombre)
        {
            var listadoPosts = (from a in _librosContext.autores
                                 join p in _librosContext.posts
                                        on a.id_autores equals p.id_autores
                                where a.nombre_autores == AutorNombre
                                 select new
                                 {
                                     a.id_autores,
                                     a.nombre_autores,
                                     p.id_posts,
                                     p.titulo_posts,
                                     p.contenido,
                                     p.fecha_publicacion
                                 }).Take(20).OrderBy(resultado => resultado.id_autores).ThenByDescending(resultado => resultado.id_posts).ToList();

            if (listadoPosts.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoPosts);
        }

        /*Método para obtener el listado de todos los posts por libro*/
        [HttpGet]
        [Route("GetLibros")]

        public IActionResult GetPosts(string PostsLibros)
        {
            var postsPorLibros = (from a in _librosContext.autores
                                    join p in _librosContext.posts
                                       on a.id_autores equals p.id_autores
                                    join al in _librosContext.autor_libro
                                       on a.id_autores equals al.id_autores
                                    join l in _librosContext.libros
                                       on al.id_libros equals l.id_libros
                                  where a.nombre_autores == PostsLibros
                                select new
                                {
                                    p.id_posts,
                                    p.titulo_posts,
                                    l.titulo_libros,
                                    p.contenido,
                                    p.fecha_publicacion,
                                    a.nombre_autores
                                }).ToList();

            if (postsPorLibros.Count == 0)
            {
                return NotFound();
            }

            return Ok(postsPorLibros);
        }

        /*Método para crear o insertar registros*/
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPosts([FromBody] Posts posts)
        {
            try
            {
                _librosContext.posts.Add(posts);
                _librosContext.SaveChanges();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Método para modificar los registros de la tabla
        [HttpPut]
        [Route("Actualizar/{id}")]

        public IActionResult ActualizarPosts(int id, [FromBody] Posts postsModificar)
        {
            Posts? postsActuales = (from e in _librosContext.posts
                                     where e.id_posts == id
                                     select e).FirstOrDefault();

            if (postsActuales == null)
            { return NotFound(); }

            postsActuales.id_posts = postsModificar.id_posts;
            postsActuales.titulo_posts = postsModificar.titulo_posts;
            postsActuales.id_autores = postsModificar.id_autores;
            postsModificar.contenido = postsModificar.contenido;
            postsModificar.fecha_publicacion = postsModificar.fecha_publicacion;

            _librosContext.Entry(postsActuales).State = EntityState.Modified;
            _librosContext.SaveChanges();

            return Ok(postsModificar);
        }

        /*Método para eliminar registros*/
        [HttpPut]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarPosts(int id)
        {
            Posts? posts = (from e in _librosContext.posts
                              where e.id_posts == id
                              select e).FirstOrDefault();

            if (posts == null)
            { return NotFound(); }

            _librosContext.posts.Attach(posts);
            _librosContext.posts.Remove(posts);
            _librosContext.SaveChanges();

            return Ok(posts);

        }
    }
}
