using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; init; }

        protected BaseEntity(Guid? Id = null)
        {
            Id = Id ?? Guid.NewGuid();
        }
    }
}
