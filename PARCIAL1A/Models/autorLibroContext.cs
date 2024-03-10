using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1A.Models
{

    public class autorLibroContext : DbContext
    {
        public autorLibroContext(DbContextOptions<autoresContext> options) : base(options)
        {
        }
        public DbSet<Autor_libro> autor_libro { get; set; }
    }
}