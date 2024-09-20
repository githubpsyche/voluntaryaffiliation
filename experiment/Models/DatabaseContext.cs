using ExperimentalGoal.Models;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesDemo.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Customer> CustomerTB { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Players> Player { get; set; }
    }
}
