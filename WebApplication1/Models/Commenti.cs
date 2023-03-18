using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Commenti")]
public class Commenti
{
    [Key, Column("Id"), ]
    public int Id { get; set; } 
    public int Reply { get; set; }
    [Required]
    public int Star { get; set; }
    [Required]
    public string Commento { get; set; } = null!;
    public bool Pin { get; set; }
}