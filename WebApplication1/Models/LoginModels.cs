using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class LoginModels
{
    public string Commento { get; set; }
    public bool Pin { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public int Id { get; set; }

    public bool TryLogin(MvcCoseinutiliContext context, string user, string pwd)
    {
        List<Utenti> lu = context.Utentis.Where(u => u.Username.Equals(user) && u.Password.Equals(pwd)).ToList();
        if (lu.Count != 1) return false;
        UtenteSingleton.GetInstance(lu[0].Id, lu[0].Username);
        this.Id = lu[0].Id;
        Console.WriteLine("Utente loggato e registrato nel singleton");
        return true;
    }
    
    public bool GuestLogin()
    {
        UtenteSingleton.GetInstance(-1, "Guest");
        Console.WriteLine("Utente loggato e registrato nel singleton");
        return true;
    }
}