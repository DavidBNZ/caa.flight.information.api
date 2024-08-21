using Microsoft.AspNetCore.Mvc;

namespace caa.flight.information.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightInformationController : ControllerBase
    {
        
        private readonly ILogger<FlightInformationController> _logger;

        public FlightInformationController(ILogger<FlightInformationController> logger)
        {
            _logger = logger;
        }
    }
}
