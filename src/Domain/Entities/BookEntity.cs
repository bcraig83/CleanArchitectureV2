﻿using CleanArchitecture.Domain.Common.Entities;
using CleanArchitecture.Domain.Common.Events;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class BookEntity : BaseEntity, IHasDomainEvent
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }

        public string Publisher { get; set; }

        public string ISBN10 { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        // TODO: add this as an example of a value object
        //public Format Format { get; set; }
    }
}