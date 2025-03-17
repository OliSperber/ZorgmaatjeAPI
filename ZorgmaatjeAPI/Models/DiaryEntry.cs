namespace ZorgmaatjeAPI.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class DiaryEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ChildId { get; set; }

    [Required]
    public string Content { get; set; }

    public int? StickerId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Date { get; set; }
}
