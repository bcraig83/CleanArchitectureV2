using CleanArchitecture.Application.Features.Books.Queries.GetBooks.Models;
using MediatR;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Features.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<IList<BookDto>>
    {
    }
}