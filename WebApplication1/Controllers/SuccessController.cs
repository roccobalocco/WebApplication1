using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.API;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SuccessController : Controller
{
    //todo: use logger
    private readonly ILogger<SuccessController> _logger;
    private readonly MvcCoseinutiliContext _context;

    public SuccessController(ILogger<SuccessController> logger, MvcCoseinutiliContext context)
    {
        _logger = logger;
        _context = context;
    }

    public ActionResult InviaCommento(LoginModels model)
    {
        Commentare cmt = new Commentare(_context);
        Debug.Assert(model.Commento != null, "model.Commento != null");
        if (cmt.InserisciCommento(model.Commento, pin: model.Pin))
            ViewData["Inserimento"] = "Inserimento avvenuto con successo!";
        ViewData["Inserimento"] = "Inserimento non avvenuto";
        //se devo rimanere sulla stessa view ritorno una stringa!
        return PartialView("Commenti");
    }
}