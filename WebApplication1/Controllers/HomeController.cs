using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    //todo: use logger
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
    public Task<IActionResult> Login(LoginModels model)
    {
        if (model is { Password: { }, Username: { } } && !ModelState.IsValid | model.Username.Length == 0 | model.Password.Length == 0)
            return Task.FromResult<IActionResult>(RedirectToAction("Index"));

        Console.WriteLine("Login per utente {0}", model.Username);
        return Task.FromResult<IActionResult>(model is { Password: { }, Username: { } } && model.TryLogin(_context, model.Username, model.Password) ? RedirectToAction("Success") : RedirectToAction("Index"));
    }

    public IActionResult Success()
    {
        ViewData["Inserimento"] = null;
        ViewData["Username"] = UtenteSingleton.GetInstance().Username;
        ViewData["Id"] = UtenteSingleton.GetInstance().Id;
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public Task<IActionResult> GuestLogin(LoginModels model)
    {
        model.GuestLogin();
        return Task.FromResult<IActionResult>(RedirectToAction("Success"));
    }
}