using Expense_Tracker.Models; // Import models
using Microsoft.AspNetCore.Mvc; // Import MVC components
using System.Diagnostics; // Import diagnostic tools

namespace Expense_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor to initialize logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Method for the Index page
        public IActionResult Index()
        {
            return View();
        }

        // Method for the Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Method for the Error page
        // Disable caching for error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create ErrorViewModel with current request ID or trace identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
