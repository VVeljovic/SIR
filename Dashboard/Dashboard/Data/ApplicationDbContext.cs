using Microsoft.EntityFrameworkCore;

namespace Dashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SensorStatsByDevice> SensorStatsByDevice { get; set; }
        public DbSet<SensorStatsByHour> SensorStatsByHour { get; set; }
        public DbSet<SensorReading> SensorReading { get; set; }
    }
}
