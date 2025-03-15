namespace ZorgmaatjeAPI.Models;

using System.ComponentModel.DataAnnotations;

public class Level
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Required]
    public int LevelNumber { get; set; }
}
