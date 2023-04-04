using System.Diagnostics;

namespace WebApplication1.Models;

public class UtenteSingleton
{
    private static UtenteSingleton? _instance; 

    private static object _mutex = new();

    public int Id;
    public string Username;
    
    private UtenteSingleton(int id, string user)
    {
        Id = id;
        Username = user;
    } 
    private UtenteSingleton()
    {
        Id = -1;
        Username = "guest";
    }
    public static UtenteSingleton? GetInstance(int id, string user)
    {
        if (_instance != null) return _instance;
        lock (_mutex) // now I can claim some form of thread safety...
            _instance ??= new UtenteSingleton(id, user);

        return _instance;
    }
    public static UtenteSingleton GetInstance()
    {
        if (_instance != null) return _instance;
        lock (_mutex) // now I can claim some form of thread safety...
            _instance ??= new UtenteSingleton();
        
        return _instance;
    }

    public static void SetInstance()
    {
        Debug.Assert(_instance != null, nameof(_instance) + " != null");
        _instance.Id = -1;
        _instance.Username = "guest";
    } 
    public static void SetInstance(int id, string user)
    {
        Debug.Assert(_instance != null, nameof(_instance) + " != null");
        _instance.Id = id;
        _instance.Username = user;
    }
}