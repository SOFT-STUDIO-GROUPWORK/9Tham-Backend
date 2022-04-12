using System.ComponentModel.DataAnnotations;

namespace Tham_Backend.Models;

public class Tag
{
    public int Id { get; set; }

    [Required] [StringLength(100)] public string Name { get; set; }
}