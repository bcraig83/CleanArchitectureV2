using CleanArchitecture.Application.Features.Books.Queries.GetBooks;
using CleanArchitecture.Application.Features.Books.Queries.GetBooks.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebUI.Pages
{
    public class GetBooksModel : PageModel
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        public IList<BookDto> Books;

        public async void OnGet()
        {
            var getBooksQuery = new GetBooksQuery();

            Books = await Mediator.Send(new GetBooksQuery());
        }
    }
}