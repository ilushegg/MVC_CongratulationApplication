using Microsoft.EntityFrameworkCore;
using MVC_CongratulationApplication.Domain.Entity;

namespace MVC_CongratulationApplication.DAL.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

    }
}
