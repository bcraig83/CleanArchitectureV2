using CleanArchitecture.Integration.Messaging.AzureServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace SandboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBusController : ControllerBase
    {
        private readonly MessageProducer _producer;

        public ServiceBusController(MessageProducer producer)
        {
            _producer = producer;
        }

        [HttpGet("health")]
        public ActionResult GetHealth()
        {
            return Ok("Service bus controller is up!");
        }
    }
}