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

            var speciality = await _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality is null)
            {
                return BadRequest("La Especialidad no existe");
            }

            var doctor = new Doctor(request.Name, request.LicenseNumber, true, speciality);
            await _persistence.SaveDoctor(doctor);

            return StatusCode(201);
        }

        [HttpGet("doctors")]
        public async Task<ActionResult<List<Doctor>>> GetAll()
        {
            var doctors = await _persistence.GetAllDoctors();
            var activeDoctors = doctors.Where(d => d.IsActive).Select(d => new
            {
                d.Id,
                d.Name,
                d.LicenseNumber,
                SpecialityName = d.Speciality?.Name ?? string.Empty
            }).ToList();

            return Ok(activeDoctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetById(Guid Id)
        {
            var doctor = await _persistence.GetDoctorById(Id);

            if (doctor == null || !doctor.IsActive)
            {
                return NotFound();
            }

            return Ok(new
            {
                doctor.Name,
                doctor.LicenseNumber,
                SpecialityName = doctor.Speciality?.Name ?? string.Empty
            });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(Guid Id)
        {
            var doctor = await _persistence.GetDoctorById(Id);
            if (doctor == null || !doctor.IsActive)
            {
                return NotFound();
            }
            await _persistence.UpdateDoctor(doctor);
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
