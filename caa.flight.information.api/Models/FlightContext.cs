using Microsoft.EntityFrameworkCore;

namespace caa.flight.information.api.Models
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options) {}

        public virtual DbSet<Flight> Flights { get; set; } = null!;
    }
}
