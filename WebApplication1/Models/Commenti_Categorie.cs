using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Commenti_Categorie")]
public class Commenti_Categorie
{
    
    [Key, ForeignKey("Categoria"), MaxLength(50), MinLength(1)]
    public string? Categoria { get; set; }
    [Key, ForeignKey("IdCommento")]
    public int IdCommento { get; set; } 
}