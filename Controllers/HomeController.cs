using Microsoft.AspNetCore.Mvc;
using move_store_app.Models;
using System.Diagnostics;

namespace move_store_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Index(string success, string successLogged, string successLoggedOut, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = "Message Sent!";
            }
            if (!string.IsNullOrEmpty(success))
            {
                ViewBag.Success = success;
            }
            if (!string.IsNullOrEmpty(successLogged))
            {
                ViewBag.SuccessLogged = successLogged;
            }
            if (!string.IsNullOrEmpty(successLoggedOut))
            {
                ViewBag.successLoggedOut = successLoggedOut;
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        
    }
}