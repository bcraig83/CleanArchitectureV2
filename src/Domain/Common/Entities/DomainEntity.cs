﻿using System;

namespace CleanArchitecture.Domain.Common.Entities
{
    public abstract class DomainEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        // TODO: public string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        // TODO: public string LastModifiedBy { get; set; }
    }
}