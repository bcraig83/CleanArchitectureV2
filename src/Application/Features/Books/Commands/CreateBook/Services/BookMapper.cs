using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Books.Commands.CreateBook.Services
{
    public class BookMapper
    {
        private readonly ApplicationOptions _options;

        public BookMapper(
            ILogger<BookMapper> logger,
            ApplicationOptions options)
        {
            logger.LogInformation($"Options: {_options}");

            _options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public BookEntity Map(CreateBookCommand command)
        {
            string author = _options.StoreAuthorInLowercase
                ? command?.Author?.ToLower()
                : command?.Author;

            var entity = new BookEntity
            {
                Title = command.Title,
                Author = author,
                Language = command.Language,
                Publisher = command.Publisher,
                ISBN10 = command.ISBN10
            };

            // TODO:
            var bookCreatedEvent = new BookCreatedEvent
            {
                Title = entity.Title,
                Author = entity.Author
            };

            entity.DomainEvents.Add(bookCreatedEvent);

            return entity;
        }
    }
}