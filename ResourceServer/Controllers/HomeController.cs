using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceServer.Models;
using System.Diagnostics;

namespace ResourceServer.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    [Route("/")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("You are admin of test_cli");
        }

    }
}