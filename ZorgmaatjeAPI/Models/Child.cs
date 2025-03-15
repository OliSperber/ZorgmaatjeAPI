namespace ZorgmaatjeAPI.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Child
{
    [Required]
    [StringLength(450)]
    public string UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [StringLength(100)]
    public string? DoctorName { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreationDate { get; set; }

    [Required]
    [RegularExpression("[AB]")]
    public char TreatmentPath { get; set; }

    public int? CharacterId { get; set; }
}
