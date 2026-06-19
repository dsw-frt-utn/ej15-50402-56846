using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = new List<Speciality>();
        private List<Doctor> _doctors = new List<Doctor>();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctors;
        }

        public void AddDoctor(Doctor doctor)
        {
            if (doctor == null)
                return;

            _doctors.Add(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.SingleOrDefault(d => d.Id == id);
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                if (!File.Exists(jsonPath))
                    return;

                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                        new JsonSerializerOptions()
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new List<SpecialityDto>();

                _specialities = specialities.Select(s => new Speciality(s.Name, s.Description, s.Id)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
