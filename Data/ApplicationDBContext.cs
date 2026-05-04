using Microsoft.EntityFrameworkCore;
using My_Port.Dto;
using My_Port.Models;

namespace My_Port.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }  
        public DbSet<ad_Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        //public DbSet<UserDto> UserDtos { get; set; } //If you want to map raw SQL results to UserDto, you can add this DbSet, but it won't be used for regular EF Core operations.



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserDto>().HasNoKey(); // required for raw SQL mapping
        //}
    }
}
