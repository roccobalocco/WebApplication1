using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class SuccessController : Controller
{
    private readonly ILogger<SuccessController> _logger;

    public SuccessController(ILogger<SuccessController> logger)
    {
        _logger = logger;
    }

}