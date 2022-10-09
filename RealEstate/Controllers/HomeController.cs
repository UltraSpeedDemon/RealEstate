using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using System.Diagnostics;

//Real Estate List - List of houses or apartments, displaying the name, square footage, value, rooms,
//images displaying each residence for sale, etc.
//The purpose is for the users to view/compare prices and differences between properties.
//Users will also have the ability to click on different properties to view more information/descriptions about them.

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}