using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ALIPC\\ALIAHMED;Initial Catalog=Test;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
        public DbSet<Employee> Employees { get; set; }
       
    }
}
