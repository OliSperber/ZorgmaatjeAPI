namespace ZorgmaatjeAPI.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ChildId { get; set; }

    [Required]
    [StringLength(100)]
    public string DoctorName { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(255)]
    public string AppointmentName { get; set; }
}
