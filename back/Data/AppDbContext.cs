using Microsoft.EntityFrameworkCore;
using KdAtrio.Models;

namespace KdAtrio.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Job> Jobs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(p => p.Jobs)
                .WithOne(j => j.Person)
                .HasForeignKey(j => j.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optionnel : configurations supplémentaires
            base.OnModelCreating(modelBuilder);
        }
    }
}
