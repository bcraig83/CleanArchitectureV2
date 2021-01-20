using System;

namespace CleanArchitecture.Domain.Common.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        // TODO: public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        // TODO: public string LastModifiedBy { get; set; }
    }
}