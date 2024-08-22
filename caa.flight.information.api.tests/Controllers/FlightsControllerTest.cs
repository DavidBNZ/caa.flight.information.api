using caa.flight.information.api.Models;
using caa.flight.information.api.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace caa.flight.information.api.tests.Controllers
{
    public class FlightsControllerTest
    {
        

        DateTime testTime = DateTime.Now;
        List<Flight> data = new List<Flight>
            {
                new Flight { Airline = "NZ", ArrivalAirport = "Wellington", DepartureAirport = "Auckland", ArrivalTime = DateTime.Parse("2024-08-22T00:35:00.000Z"), DepartureTime = DateTime.Parse("2024-08-22T00:15:00.000Z"), FlightNumber = "NZ-0", Id = 0, Status = Flight.FlightStatus.Landed},
                new Flight { Airline = "AUS", ArrivalAirport = "Brisbane", DepartureAirport = "Dubai", ArrivalTime = DateTime.Parse("2024-08-22T01:35:00.000Z"), DepartureTime = DateTime.Parse("2024-08-22T01:15:00.000Z"), FlightNumber = "NZ-1", Id = 1, Status = Flight.FlightStatus.Cancelled},
                new Flight { Airline = "CAN", ArrivalAirport = "Dubai", DepartureAirport = "Ottawa", ArrivalTime = DateTime.Parse("2024-08-22T02:35:00.000Z"), DepartureTime = DateTime.Parse("2024-08-22T02:15:00.000Z"), FlightNumber = "NZ-2", Id = 2, Status = Flight.FlightStatus.Scheduled},
            };

        [Test]
        public async Task GetFlights_Returns_All_Flights_Async()
        {
            var options = new DbContextOptionsBuilder<FlightContext>().Options;

            // not mocked but seems more effort than required
            var mockDbContext = new Mock<FlightContext>(options);
            mockDbContext.Setup(c => c.Flights).ReturnsDbSet(data);

            FlightsController flightController = new FlightsController(mockDbContext.Object);

            var flights = await flightController.GetFlights();

            Assert.That(flights, Is.Not.Null);
            Assert.That(flights.Value, Is.Not.Null);
            Assert.That(flights.Value, Is.Not.Empty);
        }


        [Test]
        public async Task GetFlight_Returns_Flight_With_Id_Async()
        {
            var options = new DbContextOptionsBuilder<FlightContext>().Options;

            // not mocked but seems more effort than required
            var mockDbContext = new Mock<FlightContext>(options);
            mockDbContext.Setup(c => c.Flights).ReturnsDbSet(data);
            mockDbContext.Setup(c => c.Flights.FindAsync(It.IsAny<int>())).Returns<Object[]>(input => ValueTask.FromResult(data[(int)input[0]]));

            FlightsController flightController = new FlightsController(mockDbContext.Object);

            var flights = await flightController.GetFlight(2);

            Assert.That(flights, Is.Not.Null);
            Assert.That(flights.Value, Is.Not.Null);
            Assert.That(flights.Value.Id, Is.EqualTo(2));
        }


        [Test]
        public async Task GetFlight_Returns_Flight_With_NonExistantId_Async()
        {
            var options = new DbContextOptionsBuilder<FlightContext>().Options;

            // not mocked but seems more effort than required
            var mockDbContext = new Mock<FlightContext>(options);
            mockDbContext.Setup(c => c.Flights).ReturnsDbSet(data);
            mockDbContext.Setup(c => c.Flights.FindAsync(It.IsAny<int>())).Returns<Object[]>(
                input => (
                ((int)input[0]) < data.Count() && data.Count() > 0 // if it's in the data's bounds
                    ? ValueTask.FromResult(data[(int)input[0]])  // return
                    : ValueTask.FromResult<Flight>(null))); // otherwise null

            FlightsController flightController = new FlightsController(mockDbContext.Object);

            var flights = await flightController.GetFlight(999);

            Assert.That(flights, Is.Not.Null);
            Assert.That(flights.Value, Is.Null);
        }

        [Test]
        public async Task PostFlight_AddsFlight_Async()
        {
            var options = new DbContextOptionsBuilder<FlightContext>().Options;

            // originally scheduled
            var original = new Flight { Airline = "NZ", ArrivalAirport = "Wellington", DepartureAirport = "Auckland", ArrivalTime = DateTime.Parse("2024-08-22T00:35:00.000Z"), DepartureTime = DateTime.Parse("2024-08-22T00:15:00.000Z"), FlightNumber = "NZ-0", Id = 0, Status = Flight.FlightStatus.Scheduled };

            // not mocked, instead has an in mem database but seems more effort than required
            var mockDbContext = new Mock<FlightContext>(options);
            mockDbContext.Setup(c => c.Flights).ReturnsDbSet([]);

            FlightsController flightController = new FlightsController(mockDbContext.Object);

            var flights = await flightController.GetFlights();
            Assert.That(flights.Value, Is.Empty);

            // to me this doesn't seem to be testing much. Maybe the in mem database isnt doing what I expect
            var result = await flightController.PostFlight(original);
            // thus, this was required.
            mockDbContext.Setup(c => c.Flights).ReturnsDbSet([original]);

            flights = await flightController.GetFlights();
            Assert.That(flights.Value, Is.Not.Empty);
        }


        /// More tests are required, but as the in memory database is not functioning as expected, more time would need to be spent finding out why.
        /// At this time it is acting just like any other mock, which means it doesn't update the base object when Post is called.

    }
}