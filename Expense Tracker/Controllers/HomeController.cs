﻿using Expense_Tracker.Models; // Import models
using Microsoft.AspNetCore.Mvc; // Import MVC components
using System.Diagnostics; // Import diagnostic tools

namespace Expense_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

<<<<<<< HEAD
<<<<<<< HEAD
        // Constructor to inject the logger dependency
=======
        // Constructor to initialize logger
>>>>>>> b3626a4 (added comment)
=======
        // Constructor to initialize logger
>>>>>>> bce3d9b1d32ceb37dee25725879f47e197d49e8c
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        // GET: Home/Index
        // This action returns the default view for the home page.
=======
        // Method for the Index page
>>>>>>> b3626a4 (added comment)
=======
        // Method for the Index page
>>>>>>> bce3d9b1d32ceb37dee25725879f47e197d49e8c
        public IActionResult Index()
        {
            return View();
        }

<<<<<<< HEAD
<<<<<<< HEAD
        // GET: Home/Privacy
        // This action returns the view for the privacy policy page.
=======
        // Method for the Privacy page
>>>>>>> b3626a4 (added comment)
=======
        // Method for the Privacy page
>>>>>>> bce3d9b1d32ceb37dee25725879f47e197d49e8c
        public IActionResult Privacy()
        {
            return View();
        }

<<<<<<< HEAD
<<<<<<< HEAD
        // GET: Home/Error
        // This action returns the error view when an unhandled exception occurs.
        // It includes a ResponseCache attribute to prevent caching the error page.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates an ErrorViewModel instance with the current request ID or the HTTP context trace identifier.
=======
        // Method for the Error page
        // Disable caching for error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create ErrorViewModel with current request ID or trace identifier
>>>>>>> b3626a4 (added comment)
=======
        // Method for the Error page
        // Disable caching for error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Create ErrorViewModel with current request ID or trace identifier
>>>>>>> bce3d9b1d32ceb37dee25725879f47e197d49e8c
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
