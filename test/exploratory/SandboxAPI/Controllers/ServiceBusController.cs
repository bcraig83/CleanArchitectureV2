using CleanArchitecture.Application.Features.Books.Commands.CreateBook;
using CleanArchitecture.Integration.Messaging;
using CleanArchitecture.Integration.Messaging.AzureServiceBus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Message<CreateBookCommand> command)
        {
            Console.WriteLine($"Attempting to put message onto queue. Id: {command.Id}. Type: {command.Type}");

            await _producer.Publish("BookWorm", command);

            return Ok();
        }
    }
}