using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Interfacce;

public interface Commentare
{
    //insert, modify, delete, reply
    public bool InserisciCommento(string commento);
    public bool ModificaCommento(int id, string commento);
    public bool EliminaCommento(int id);
    public bool RispondiCommento(int id, string commento);

    //utility per sopra
    public List<int> FindTags(string commento);
    public bool TaggaUtente(int id);
    public bool EliminaTag(int id);
    public bool AggiornaTag();
    
    //ottieni commenti con vari paramentri
    public DbSet<Commenti> GetCommenti();
}