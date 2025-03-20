using Microsoft.AspNetCore.Mvc;
using ZorgmaatjeAPI.Services;
using ZorgmaatjeAPI.Repositories;
using ZorgmaatjeAPI.Models;
using System.Threading.Tasks;

namespace ZorgmaatjeAPI.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly AuthorizationService _authorizationService;

    // Constructor for dependency injection
    public AppointmentController(AppointmentRepository appointmentRepository, AuthorizationService authorizationService)
    {
        _appointmentRepository = appointmentRepository;
        _authorizationService = authorizationService;
    }

    // GET: api/appointments
    [HttpGet]
    public async Task<IActionResult> GetAppointments()
    {
        // Querying for the guardian
        IEnumerable<Appointment> appointments = await _appointmentRepository.GetAll();

        // Returning the found appointments
        return Ok(appointments);
    }

    // POST: api/appointments
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Creating guardian
        try
        {
            var createdEntry = await _appointmentRepository.CreateAsync(appointment);

            return Created(string.Empty, createdEntry);
        }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex.Message}"); }
    }

    // PUT: api/appointments
    [HttpPut]
    public async Task<IActionResult> UpdateAppointment([FromBody] Appointment appointment)
    {
        // Checks if ModelState is valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Check if appointment Exists
        if (await _appointmentRepository.GetByIdAsync(appointment.Id) == null)
            return NotFound();

        // Check if user is authorized to modify the appointment
        if (!await _authorizationService.IsUserAuthorizedForEntityAsync("Appointments", appointment.Id))
            return Forbid();

        try { await _appointmentRepository.UpdateAsync(appointment); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex.Message}"); }

        return Ok(appointment);
    }

    // DELETE: api/appointments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(string id)
    {
        // Check if Appointment Exists
        if (await _appointmentRepository.GetByIdAsync(id) == null)
            return NotFound();

        // Check if user is authorized to modify the Appointment
        if (!await _authorizationService.IsUserAuthorizedForEntityAsync("Appointments", id))
            return Forbid();

        // Deleting Appointment
        try { await _appointmentRepository.DeleteAsync(id); }
        catch (Exception ex) { return StatusCode(500, $"An unexpected error occurred: {ex.Message}"); }

        return NoContent();
    }
}
