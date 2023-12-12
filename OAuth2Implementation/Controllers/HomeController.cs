using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace OAuth2Implementation.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Json(new { YES = "IT'S ME" });
        }


    }
}