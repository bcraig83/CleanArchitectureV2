using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class BookEntity : DomainEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public string ISBN10 { get; set; }

        // TODO: add this as an example of a value object
        //public Format Format { get; set; }
    }
}