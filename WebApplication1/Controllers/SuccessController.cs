using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class SuccessController : Controller
{
    private readonly ILogger<SuccessController> _logger;
    private readonly MvcCoseinutiliContext _context;

    public SuccessController(ILogger<SuccessController> logger, MvcCoseinutiliContext context)
    {
        _logger = logger;
        _context = context;
    }

}