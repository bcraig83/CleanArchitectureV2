using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Common
{
    public abstract class DomainEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();

        // TODO: public string LastModifiedBy { get; set; }
        // TODO: public string CreatedBy { get; set; }
    }
}