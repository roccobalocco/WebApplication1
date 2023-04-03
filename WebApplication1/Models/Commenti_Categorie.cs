using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Commenti_Categorie")]
[PrimaryKey(nameof(Categoria), nameof(IdCommento))]
public class Commenti_Categorie
{
    
    [ForeignKey("Categoria"), MaxLength(50), MinLength(1)]
    public string? Categoria { get; set; }
    [ForeignKey("IdCommento")]
    public int IdCommento { get; set; } 
}