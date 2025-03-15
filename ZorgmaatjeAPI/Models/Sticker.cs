namespace ZorgmaatjeAPI.Models;

using System.ComponentModel.DataAnnotations;

public class Sticker
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string StickerName { get; set; }
}
