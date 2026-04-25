using Microsoft.EntityFrameworkCore;
using My_Port.Models;

namespace My_Port.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }  
    }
}
