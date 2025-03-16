using EmployeeManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagmentSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }

        DbSet<UserLogin> UserLogins { get; set; }
    }

}
