using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;

[Table("Categorie")]
public class Categorie
{
    [Key, MaxLength(50), MinLength(1)]
    public string? Categoria { get; set; }
}