using Microsoft.EntityFrameworkCore;

namespace caa.flight.information.api.Models
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options) {}

        public DbSet<Flight> Flights { get; set; } = null!;
    }
}
