using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Speciality? GetSpecialityById(Guid id);
        IEnumerable<Doctor> GetAllDoctors();
        void AddDoctor(Doctor doctor);
        Doctor? GetDoctorById(Guid id);
    }
}

