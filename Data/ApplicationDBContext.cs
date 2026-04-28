using Microsoft.EntityFrameworkCore;
using My_Port.Models;

namespace My_Port.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<ad_Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
