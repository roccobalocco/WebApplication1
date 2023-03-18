using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.API;

public class Statistiche
{
    public int NumeroCommenti(int idUtente)
    {
        return Commentare.GetCommenti(idUtente).Count();
    }

    public int NumeroTag(int idUtente)
    {
        return Commentare.GetCommentiTag(idUtente).Count();
    }

    public int ComparizioniTotali(int idUtente)
    {
        return NumeroCommenti(idUtente) + NumeroTag(idUtente);
    }

    public DbSet<Commenti> MiglioriCommenti()
    {
        throw new NotImplementedException();
    }

    public DbSet<Commenti> PeggioriCommenti()
    {
        throw new NotImplementedException();
    }
}