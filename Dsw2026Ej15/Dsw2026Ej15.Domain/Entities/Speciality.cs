using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    internal class Speciality : BaseEntity
    {
        public string Name { get; init; }
        public string Description { get; init; }

        public Speciality(string name, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Description = description;
        }
    }
}
