using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Features.Books.Queries.GetBooks.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Books.Queries.GetBooks
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IList<BookDto>>
    {
        private readonly IRepository<BookEntity> _repository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(
            IRepository<BookEntity> repository,
            IMapper mapper)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        public async Task<IList<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var result = new List<BookDto>();

            var booksInDatabase = await _repository.GetAllAsync();

            foreach (var book in booksInDatabase)
            {
                var mappedBook = _mapper.Map<BookDto>(book);
                result.Add(mappedBook);
            }

            return result;
        }
    }
}