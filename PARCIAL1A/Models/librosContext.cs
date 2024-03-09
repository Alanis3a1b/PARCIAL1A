using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1A.Models
{

    public class librosContext : DbContext
    {
        public librosContext(DbContextOptions<librosContext> options) : base(options)
        {
        }
        public DbSet<Libros> libros { get; set; }
        public DbSet<Autores> autores { get; set; }
        public DbSet<Posts> posts { get; set; }
        public DbSet<Autor_libro> estados_equipo { get; set; }
    }
}
