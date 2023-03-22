using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MvcCoseinutiliContext _context;

    public HomeController(ILogger<HomeController> logger, MvcCoseinutiliContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModels model)
    {
        if (!ModelState.IsValid | model.Username.Length == 0 | model.Password.Length == 0)
            return RedirectToAction("Index");

        Console.WriteLine("Login per utente {0}", model.Username);
        if (model.TryLogin(_context, model.Username, model.Password))
            return RedirectToAction("Success");

        return RedirectToAction("Index");
    }

    public IActionResult Success()
    {
        ViewData["Username"] = UtenteSingleton.GetInstance().Username;
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult GuestLogin()
    {

        return RedirectToAction("Success");
    }
}