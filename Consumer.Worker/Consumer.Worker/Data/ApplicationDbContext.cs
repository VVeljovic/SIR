using Microsoft.EntityFrameworkCore;
using Producer.Domain.Models;

namespace Consumer.Worker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SensorReading> SensorReading { get; set; }

        public DbSet<SensorStatsByDevice> SensorStatsByDevice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
