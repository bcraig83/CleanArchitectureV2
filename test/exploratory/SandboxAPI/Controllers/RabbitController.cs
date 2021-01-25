using CleanArchitecture.Application.Features.Books.Commands.CreateBook;
using CleanArchitecture.Integration.Messaging;
using CleanArchitecture.Integration.Messaging.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SandboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly MessageProducer _producer;

        public RabbitController(MessageProducer producer)
        {
            _producer = producer;
        }

        [HttpGet("health")]
        public ActionResult GetHealth()
        {
            return Ok("Rabbit controller is up!");
        }

        [HttpPost]
        public ActionResult Post([FromBody] Message<CreateBookCommand> command)
        {
            Console.WriteLine($"Attempting to put message onto queue. Id: {command.Id}. Type: {command.Type}");

            _producer.Publish("BookWorm", command);

            return Ok();
        }
    }
}