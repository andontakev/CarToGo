using CarToGo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarToGo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Index action that returns the home page view
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Privacy action that returns the privacy page view
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Error action that returns the error page
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
