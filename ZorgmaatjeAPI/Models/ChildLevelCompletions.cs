namespace ZorgmaatjeAPI.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ChildLevelCompletion
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int LevelId { get; set; }

    public string ChildId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CompletionDate { get; set; }

    public int? StickerId { get; set; }
}
