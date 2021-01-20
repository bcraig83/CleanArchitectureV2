using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Features.Books.Queries.GetBooks.Models
{
    public class BookDto : IMapFrom<BookEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public string ISBN10 { get; set; }
    }
}