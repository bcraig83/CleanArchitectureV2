using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Books.Events
{
    public class BookCreatedEventHandler : IBaseEventHandler<BookCreatedEvent>
    {
        private readonly IEmailSender _emailSender;

        public BookCreatedEventHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new System.ArgumentNullException(nameof(emailSender));
        }

        public async Task Handle(
            BookCreatedEvent notification,
            CancellationToken cancellationToken)
        {
            await _emailSender.SendEmailAsync(
                 "recipient@somedomain.com",
                 "sender@somedomain.com",
                 "A new book has been added!",
                 $"Check out {notification.Title} by {notification.Author} !");
        }
    }
}