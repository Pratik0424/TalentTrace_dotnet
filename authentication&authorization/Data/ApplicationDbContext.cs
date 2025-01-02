using authentication_authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace authentication_authorization.Data
{
    public class ApplicationDbContext : DbContext
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
                    Email = "pratik@gmail.com",
                    Password = "Admin",
                });
        } 
    }
}

