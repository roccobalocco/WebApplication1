namespace WebApplication1.Models;

public class UtenteSingleton
{
    private static UtenteSingleton _instance = null; 

    private static Object _mutex = new Object();

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
    public static UtenteSingleton GetInstance(int id, string user)
    { 
        if (_instance == null) 
        { 
            lock (_mutex) // now I can claim some form of thread safety...
            {
                if (_instance == null) 
                { 
                    _instance = new UtenteSingleton(id, user);
                }
            } 
        }

        return _instance;
    }
    public static UtenteSingleton GetInstance()
    { 
        if (_instance == null) 
        { 
            lock (_mutex) // now I can claim some form of thread safety...
            {
                if (_instance == null) 
                { 
                    _instance = new UtenteSingleton();
                }
            } 
        }

        return _instance;
    }
}