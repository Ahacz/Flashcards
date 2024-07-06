using Flashcards.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<FlashCard> Flashcards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlashCard>().HasData(
                new FlashCard { Id = Guid.NewGuid(), Title = "Fibonacci", Description = "Make an algorithm that accepts an integer and returns the nth number of fibonacci series", SnapshotPath = @"f0a5f63d-6a35-4b05-abfa-84f6905c79b0" },
                new FlashCard { Id = Guid.NewGuid(), Title = "Binary search", Description = "Make an algorithm that finds a value in an (un)sorted array in fewest steps possible", SnapshotPath = @"f0a5f63d-6a35-4b05-abfa-84f6905c79b0" },
                new FlashCard { Id = Guid.NewGuid(), Title = "Mail sender", Description = "Implement a simple mail sending piece of code", SnapshotPath = @"f0a5f63d-6a35-4b05-abfa-84f6905c79b0" }
            );
        }
    }
}
