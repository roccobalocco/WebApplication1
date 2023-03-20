using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.API;

public class Statistiche
{
    private readonly MvcCoseinutiliContext _context;
    
    public Statistiche(MvcCoseinutiliContext mcc)
    {
        _context = mcc;
    }
    
    /// <summary>
    /// Ottieni il numero di commenti che un utente ha pubblicato 
    /// </summary>
    /// <param name="idUtente">Identificativo dell'utente nel database</param>
    /// <returns>Numero di commenti</returns>
    public int NumeroCommenti(int idUtente)
    {
        return (new Commentare(_context).GetCommenti(idUtente) ?? throw new InvalidOperationException()).Count();
    }
    
    /// <summary>
    /// Ottieni il numero di commenti in cui un utente è stato taggato
    /// </summary>
    /// <param name="idUtente">Identificativo dell'utente nel database</param>
    /// <returns>Numero di tag</returns>
    public int NumeroTag(int idUtente)
    {
        return new Commentare(_context).GetCommentiTag(idUtente).Count();
    }
    /// <summary>
    /// Ottieni il numero di commenti che un utente ha pubblicato in aggiunta a quelli in cui è stato taggato
    /// </summary>
    /// <param name="idUtente">Identificativo dell'utente nel database</param>
    /// <returns>Numero di tag e di commenti pubblicati</returns>
    public int ComparizioniTotali(int idUtente)
    {
        return NumeroCommenti(idUtente) + NumeroTag(idUtente);
    }

    private double GetMedia()
    {
        return _context.Commentis.Select(c => c.Star).Average();
    }
    
    /// <summary>
    /// Ottieni l'insieme di commenti migliori, a seconda della media totale dei commenti
    /// </summary>
    /// <returns>I commenti con numero di Star superiore alla media</returns>
    public DbSet<Commenti>? MiglioriCommenti()
    {
        return _context.Commentis.Where(c => c.Star > GetMedia()) as DbSet<Commenti>;
    }

    /// <summary>
    /// Ottieni l'insieme di commenti migliori, a seconda della media totale dei commenti
    /// </summary>
    /// <returns>I commenti con numero di Star inferiore o uguale alla media</returns>
    public DbSet<Commenti>? PeggioriCommenti()
    {
        return _context.Commentis.Where(c => c.Star <= GetMedia()) as DbSet<Commenti>;
    }

    /// <summary>
    /// Ottieni il numero di Star che un utente ha totalizzato in tutti i suoi commenti
    /// </summary>
    /// <param name="idUtente">Identificativo dell'utente nel database</param>
    /// <returns>Numero di star ottenute dall'utente</returns>
    private int GetStarUtente(int idUtente)
    {
        return _context.Commentis.Where(c => c.IdUtente == idUtente).Select(c => c.Star).Count();
    }
}
