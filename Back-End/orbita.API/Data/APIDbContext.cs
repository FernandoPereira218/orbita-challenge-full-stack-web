using Microsoft.EntityFrameworkCore;
using orbita.API.Models;

namespace orbita.API.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) 
        {
        
        }

        public DbSet<Student> Students { get; set; }
    }
}
