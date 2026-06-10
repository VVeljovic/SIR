using Microsoft.EntityFrameworkCore;
using Producer.Domain.Entities;

namespace Consumer.Worker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AccidentRecord> AccidentRecords { get; set; }

        public DbSet<AccidentByState> AccidentsByState { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<AccidentByState>()
                .HasIndex(x => x.State)
                .IsUnique();
        }
    }
}
