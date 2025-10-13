using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ObsidianInk.Models;

namespace ObsidianInk.Data
{
    public class ObsidianInkContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ObsidianInkContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ObsidianInkConfig"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration can go here
        }
    }
}
