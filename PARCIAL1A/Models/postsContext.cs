using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1A.Models
{

    public class postsContext : DbContext
    {
        public postsContext(DbContextOptions<autoresContext> options) : base(options)
        {
        }
    }
}