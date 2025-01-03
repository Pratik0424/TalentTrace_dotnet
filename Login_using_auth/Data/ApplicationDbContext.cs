using Login_using_auth.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_using_auth.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Pratik",
                    Username = "Pratik",
                    Email = "pratik@gmail.com",
                    Password = "Admin@123",
                });
        }
    }
}
