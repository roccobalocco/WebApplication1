using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Tag_Utenti")]
[PrimaryKey(nameof(IdUtente), nameof(IdCommento))]
public class Tag_Utenti
{
    [ForeignKey("IdUtente")]
    public int IdUtente { get; set; }
    [ForeignKey(("IdCommento"))]
    public int IdCommento { get; set; }
}