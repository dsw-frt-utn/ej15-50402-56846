using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        /*Speciality? GetSpecialityById(Guid id);
        IEnumerable<Doctor> GetAllDoctors();
        void AddDoctor(Doctor doctor);
        Doctor? GetDoctorById(Guid id);*/

        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<Doctor?> GetDoctorById(Guid id);
        Task<Speciality?> GetSpecialityById(Guid id);
        Task SaveDoctor(Doctor doctor);
        Task UpdateDoctor(Doctor doctor);
    }
}

