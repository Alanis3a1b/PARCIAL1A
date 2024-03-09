using System.ComponentModel.DataAnnotations;
using System;

namespace PARCIAL1A.Models
{
    public class Libros
    {
        [Key]

        public int id_libros { get; set; }
        public string? titulo_libros { get; set; }

    }
    public class Autores
    {
        [Key]

        public int id_autores { get; set; }
        public string? nombre_autores { get; set; }

    }

    public class Posts
    {
        [Key]

        public int id_posts { get; set; }
        public string? titulo_posts { get; set; }
        public string? contenido { get; set; }
        public DateTime fecha_publicaacion { get; set; }
        public int id_autores { get; set; }


    }

    public class Autor_libro
    {
        [Key]

        public int id_autores { get; set; }
        public int id_libros { get; set; }
        public int orden { get; set; }

    }
}
