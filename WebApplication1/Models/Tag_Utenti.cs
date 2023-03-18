using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models;

[Table("Tag_Utenti")]
public class Tag_Utenti
{
    [Key, ForeignKey("IdUtente")]
    public int IdUtente { get; set; }
    [Key, ForeignKey(("IdCommento"))]
    public int IdCommento { get; set; }
}