using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Expense_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor to inject the logger dependency
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home/Index
        // This action returns the default view for the home page.
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/Privacy
        // This action returns the view for the privacy policy page.
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Error
        // This action returns the error view when an unhandled exception occurs.
        // It includes a ResponseCache attribute to prevent caching the error page.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates an ErrorViewModel instance with the current request ID or the HTTP context trace identifier.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
