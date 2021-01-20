using CleanArchitecture.Application.Features.Books.Commands.CreateBook;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace WebUI.Pages
{
    public class BooksModel : PageModel
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            var command = new CreateBookCommand
            {
                Title = Request.Form["title"],
                Author = Request.Form["author"],
                Language = Request.Form["language"],
                Publisher = Request.Form["publisher"],
                ISBN10 = Request.Form["isbn10"]
            };

            await Mediator.Send(command);
        }
    }
}