using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NuGet.Packaging.Signing;
using WebApplication1.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace WebApplication1.API;

public class Commentare
{
    private readonly MvcCoseinutiliContext _context;
    public Commentare(MvcCoseinutiliContext mcc)
    {
        _context = mcc;
    }
    
    
    /// <summary>
    /// Inserisci un commento specificandone i parametri 
    /// </summary>
    /// <param name="commento">Stringa da pubblicare come commento</param>
    /// <param name="categorie">Lista di categorie a cui il commento appartiene - campo opzionale default null</param>
    /// <param name="pin">Valore binario che determina se fissare il commento - campo opzionale default false</param>
    /// <param name="idReply">Identificativo del commento a cui si risponde - campo opzionale di default -1</param>
    /// <returns>true se l'inserimento è andato a buon fine, false altrimenti</returns>
    public bool InserisciCommento(string commento, List<string> categorie = null, bool pin = false, int idReply = -1)
    {
        try
        {
            Commenti com = new Commenti();
            com.Id = NextIdCommento();
            com.Commento = commento;
            com.Pin = pin;
            com.Star = 0;
            com.IdUtente = UtenteSingleton.GetInstance()!.Id;
            com.Pubblicazione = new DateTime();
            com.Reply = com.Id;
            if (idReply != -1)
                com.Reply = idReply;
            //TODO: categorie!!!!!!!!
            
            _context.Add(com);
            return 1 == _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }
    
    /// <summary>
    /// Modifica un commento pre-esistente specificandone i parametri 
    /// </summary>
    /// <param name="idCommento">Identificativo del commento nel database</param>
    /// <param name="commento">Lista di categorie a cui il commento appartiene - campo opzionale default null</param>
    /// <param name="pin">Valore binario che determina se fissare il commento - campo opzionale default false</param>
    /// <returns>true se l'inserimento è andato a buon fine, false altrimenti</returns>
    public bool ModificaCommento(int idCommento, string commento, bool pin = false)
    {
        try{
            Commenti com = _context.Commentis.First(c => c.Id == idCommento);
            if (com == null) throw new NullReferenceException("Commento inesistente");
            com.Commento = commento;
            com.Pin = pin;

            _context.Entry(com).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }
    
    /// <summary>
    /// Elimina un commento pre-esistente, i tag ad esso collegati e anche le riposte a tale commento 
    /// </summary>
    /// <param name="idCommento">Identificativo del commento nel database</param>
    /// <returns>true se la cancellazione è andato a buon fine, false altrimenti</returns>
    public async Task<bool> EliminaCommento(int idCommento)
    {
        try
        {
            Commenti com = _context.Commentis.First(c => c.Id == idCommento);

            _context.Remove(com);
            
            await _context.SaveChangesAsync();
            return await EliminaCommentiCollegati(idCommento) && await EliminaTag(idCommento);
        }catch(Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    private async Task<bool> EliminaCommentiCollegati(int idCommento)
    {
        try
        {
            foreach (var com in _context.Commentis.Where(c => c.Reply == idCommento))
            {
                _context.Remove(com);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }
    
    /// <summary>
    /// Inserisci un commento come risposta di un altro, specificandone i parametri 
    /// </summary>
    /// <param name="idCommento">Identificativo del commento a cui rispondere nel database</param>
    /// <param name="commento">Stringa da pubblicare come commento</param>
    /// <param name="categorie">Lista di categorie a cui il commento appartiene - campo opzionale default null</param>
    /// <param name="pin">Valore binario che determina se fissare il commento - campo opzionale default false</param>
    /// <param name="idReply">Identificativo del commento a cui si risponde - campo opzionale di default -1</param>
    /// <returns>true se l'inserimento è andato a buon fine, false altrimenti</returns>
    public bool RispondiCommento(int idCommento, string commento)
    {
        return InserisciCommento(commento, idReply:idCommento);
    }

    //Utility per sopra
    //Trova prossimo id da inserire, riguardo tutte le tabelle possibili:
    private int NextIdUtente()
    {
        try
        {
            return _context.Utentis.Max(u => u.Id) + 1;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Primo commento inserito");
            return 1;
        }
    }
    private int NextIdCommento()
    {
        try{
            return _context.Commentis.Max(c => c.Id) + 1;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Primo commento inserito");
            return 1;
        }
    }

    private int FindIdUtente(string username)
    {
        return _context.Utentis.First(u => u.Username != null && u.Username.Equals(username)).Id;
    }

    //Trova tutti i tag all'interno di un commento, partendo dal presupposta che gli username sono univoci
    //Id viene quindi mantenuto per avere un valore numerico da utilizzare come chiave esterna e migliorare spazio occupato, ma solo per questo motivo.
    public List<int> FindTags(string commento)
    {
        List<int> tagList = new List<int>();
        var tags = commento.Split(" ").Where(s => s[0].Equals('@'));
        foreach (var tag in tags)
            tagList.Add(FindIdUtente(tag.Substring(1)));
        return tagList;
    }

    public bool TaggaUtente(int idCommento, int idUtente)
    {
        try
        {
            Tag_Utenti tu = new Tag_Utenti();
            tu.IdCommento = idCommento;
            tu.IdUtente = idUtente;

            _context.Add(tu);
            return 1 == _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    /// <summary>
    /// Elimina tutti i tag associati ad un commento 
    /// </summary>
    /// <param name="idCommento">Identificativo del commento nel database</param>
    /// <returns>true se l'inserimento è andato a buon fine, false altrimenti</returns>
    public async Task<bool> EliminaTag(int idCommento)
    {
        try
        {
            foreach (var tag in _context.TagUtentis.Where(tag => tag.IdCommento == idCommento))
                _context.Remove(tag);

            await _context.SaveChangesAsync();
            return true;
        }catch(Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    //Riaggiorna i tag presenti in un commento, dopo il suo aggiornamento
    public bool AggiornaTag(int idCommento)
    {
        try{
            foreach (var idUtente in FindTags(_context.Commentis.First(c => c.Id == idCommento).Commento))
            {
                Tag_Utenti tu = new Tag_Utenti();
                tu.IdCommento = idCommento;
                tu.IdUtente = idUtente;
                _context.Add(tu);
            }

            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }
    
    public bool StarCommento(int idCommento, int quantityOfStar = 1)
    {
        try{
            Commenti com = _context.Commentis.First(c => c.Id == idCommento);
            if (com == null) throw new NullReferenceException("Commento inesistente");
            com.Star += quantityOfStar;

            _context.Entry(com).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    public bool UnStarCommento(int idCommento)
    {
        return StarCommento(idCommento, -1);
    }
    
    public bool PinCommento(int idCommento, bool pin = true)
    {
        try{
            Commenti com = _context.Commentis.First(c => c.Id == idCommento);
            if (com == null) throw new NullReferenceException("Commento inesistente");
            com.Pin = pin;

            _context.Entry(com).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return false;
        }
    }

    public bool UnpinCommento(int idCommento)
    {
        return PinCommento(idCommento, false);
    }

    //Ottieni commenti con vari paramentri

    public DbSet<Commenti> GetCommenti()
    {
        return _context.Commentis;
    }

    public string? GetUsername(int idUtente)
    {
        return _context.Utentis.First(u => u.Id == idUtente).Username;
    }

    public DbSet<Commenti>? GetCommenti(int idUtente)
    {
        return _context.Commentis.Where(c => c.IdUtente == idUtente) as Microsoft.EntityFrameworkCore.DbSet<Commenti>;
    }

    //Ottieni commenti in cui un certo utente è stato taggato
    public DbSet<Commenti> GetCommentiTag(int idUtente)
    {
        var commenti = _context.Commentis.Join(
            _context.TagUtentis,
            c => c.Id, tu => tu.IdCommento,
            (c, tu) => new
            {
                IdUtente = tu.IdUtente, IdCommento1 = c.Id, Commento = c.Commento, Pin = c.Pin,
                Star = c.Star, Reply = c.Reply, Pubblicazione = c.Pubblicazione, IdUtenteOrigine = c.IdUtente
            }
        ).Join(
            _context.Utentis,
            tu => tu.IdUtente, u => u.Id,
            (tu, u) => new
            {
                IdCommento = tu.IdCommento1, Commento = tu.Commento, Star = tu.Star, Reply = tu.Reply,
                Pin = tu.Pin, Pubblicazione = tu.Pubblicazione, IdUtente = u.Id
            }
        ).Where(c => c.IdUtente == idUtente).ToList();
        DbSet<Commenti> result = new InternalDbSet<Commenti>(_context, "Commenti");
        foreach (var com in commenti)
        {
            Commenti tmp = new Commenti(); 
            tmp.Id = com.IdCommento; tmp.Commento = com.Commento; tmp.Star = com.Star; 
            tmp.Reply = com.Reply; tmp.Pin = com.Pin; tmp.Pubblicazione = com.Pubblicazione;
            result.Add(tmp);
        }
        return result;
    }

    public IOrderedQueryable<Commenti> GetCommentiPinned()
    {
        return _context.Commentis.Where(c => c.Pin).OrderBy(c => c.Pubblicazione);
    }

    public IQueryable<Commenti> GetReplies(int idCommento)
    {
        return _context.Commentis.Where(cmt => cmt.Id != idCommento && cmt.Reply == idCommento);
    }
}