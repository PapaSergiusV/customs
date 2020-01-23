using Microsoft.EntityFrameworkCore;
using customs.Models;

namespace customs
{
    public class Context : DbContext
    {
        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public Context() {}
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=customs;Username=postgres;Password=postgres");
        }
    }
}
