using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost("doctors")]
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y Matrícula son requeridos");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
            {
                return BadRequest("La Especialidad no existe");
            }

            var doctor = new Doctor(request.Name, request.LicenseNumber, true, speciality);
            _persistence.AddDoctor(doctor);

            return StatusCode(201);
        }

        [HttpGet("doctors")]
        public IActionResult GetActiveDoctors()
        {
            var doctors = _persistence.GetAllDoctors()
                .Where(d => d.IsActive)
                .Select(d => new {
                    d.Id,
                    d.Name,
                    d.LicenseNumber,
                    SpecialityName = d.Speciality?.Name
                })
                .ToList();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            var response = new DoctorResponseDto
            {
                Name = doctor.Name,
                LicenseNumber = doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name ?? "Sin especialidad"
            };

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistence.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            doctor.IsActive = false;

            return NoContent();
        }
    }

    public class CreateDoctorDto
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public Guid SpecialityId { get; set; }
    }

    public class DoctorResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string SpecialityName { get; set; } = string.Empty;
    }
}
