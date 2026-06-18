using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    internal class Doctor : BaseEntity
    {
        public string Name { get; init; }
        public string LicenseNumber { get; init; }
        public bool IsActive { get; private set; }
        public Speciality Speciality { get; private set; }

        public Doctor(string name, string licenseNumber, bool isActive, Speciality speciality)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            IsActive = true;
            Speciality = speciality;
        }
    }
}
