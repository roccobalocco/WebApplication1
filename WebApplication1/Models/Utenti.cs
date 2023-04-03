using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Utenti")]
public class Utenti
{
    [Key, Column("Id")]
    public int Id { get; set; }
    [Column("Username"), Required, MaxLength(50, ErrorMessage = "Max 50 chars for Username")]
    public string? Username { get; set; }
    [Column("Password"), Required, MaxLength(50, ErrorMessage = "Max 50 chars for Username")]
    public string? Password { get; set; }
    [Column("Admin"), Required]
    public bool Admin { get; set; }
}