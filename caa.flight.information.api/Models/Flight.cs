namespace caa.flight.information.api.Models
{
    public class Flight
    {
        public virtual int Id { get; set; }
        public virtual string FlightNumber { get; set; }

        public virtual string Airline { get; set; }

        public virtual  string DepartureAirport { get; set; }

        public virtual string ArrivalAirport { get; set; }

        public virtual DateTime DepartureTime { get; set; }

        public virtual DateTime ArrivalTime { get; set; }

        public virtual FlightStatus Status { get; set; }

        public enum FlightStatus
        {
            Scheduled,
            Delayed,
            Cancelled,
            InAir,
            Landed
        }
    }
}
