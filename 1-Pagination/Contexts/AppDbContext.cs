using _1_Pagination.Models;
using Microsoft.EntityFrameworkCore;

namespace _1_Pagination.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

        
    }
}
