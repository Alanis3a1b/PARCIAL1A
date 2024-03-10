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
        private readonly postsContext _postsContext;

        public postsController(postsContext postsContext)
        {
            _postsContext = postsContext;
        }

        /*Para obtener registros*/
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<Posts> listadoPosts = (from e in _postsContext.posts
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
            Posts? posts = (from e in _postsContext.posts
                              where e.id_posts == id
                              select e).FirstOrDefault();

            if (posts == null)
            {
                return NotFound();

            }

            return Ok(posts);
        }

        /*Método para crear o insertar registros*/
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarPosts([FromBody] Posts posts)
        {
            try
            {
                _postsContext.posts.Add(posts);
                _postsContext.SaveChanges();
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
            Posts? postsActuales = (from e in _postsContext.posts
                                     where e.id_posts == id
                                     select e).FirstOrDefault();

            if (postsActuales == null)
            { return NotFound(); }

            postsActuales.id_posts = postsModificar.id_posts;
            postsActuales.titulo_posts = postsModificar.titulo_posts;
            postsActuales.id_autores = postsModificar.id_autores;
            postsModificar.contenido = postsModificar.contenido;
            postsModificar.fecha_publicacion = postsModificar.fecha_publicacion;

            _postsContext.Entry(postsActuales).State = EntityState.Modified;
            _postsContext.SaveChanges();

            return Ok(postsModificar);
        }

        /*Método para eliminar registros*/
        [HttpPut]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarPosts(int id)
        {
            Posts? posts = (from e in _postsContext.posts
                              where e.id_posts == id
                              select e).FirstOrDefault();

            if (posts == null)
            { return NotFound(); }

            _postsContext.posts.Attach(posts);
            _postsContext.posts.Remove(posts);
            _postsContext.SaveChanges();

            return Ok(posts);

        }
    }
}
