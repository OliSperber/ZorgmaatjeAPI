namespace ZorgmaatjeAPI.Models;

using System.ComponentModel.DataAnnotations;

public class Guardian
{
    [Required]
    [StringLength(450)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(450)]
    public string UserId { get; set; }

    [Required]
    [StringLength(450)]
    public string ChildId { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; }

    [StringLength(20)]
    [Phone]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }
}
