using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.API;

public class Commentare
{
    //Insert, modify, delete, reply
    
    public bool InserisciCommento(string commento, List<string> categorie, bool pin = false)
    {
        throw new NotImplementedException();
    }

    public bool ModificaCommento(int idCommento, string commento)
    {
        throw new NotImplementedException();
    }

    public bool EliminaCommento(int idCommento)
    {
        throw new NotImplementedException();
    }

    public bool RispondiCommento(int idCommento, string commento)
    {
        throw new NotImplementedException();
    }

    //Utility per sopra
    
    //Trova tutti i tag all'interno di un commento
    public List<int> FindTags(string commento)
    {
        throw new NotImplementedException();
    }

    public bool TaggaUtente(int idCommento, int idUtente)
    {
        throw new NotImplementedException();
    }

    public bool EliminaTag(int idCommento)
    {
        throw new NotImplementedException();
    }

    //Riaggiorna i tag presenti in un commento, dopo il suo aggiornamento
    public bool AggiornaTag(int idCommento)
    {
        throw new NotImplementedException();
    }

    //Ottieni commenti con vari paramentri

    public DbSet<Commenti> GetCommenti()
    {
        return null;
    }

    public DbSet<Commenti> GetCommenti(int idUtente)
    {
        throw new NotImplementedException();
    }

    //Ottieni commenti in cui un certo utente è stato taggato
    public DbSet<Commenti> GetCommentiTag(int idUtente)
    {
        throw new NotImplementedException();
    }
}