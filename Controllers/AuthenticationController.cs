using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using move_store_app.data;
using move_store_app.Models;
using NuGet.Protocol;

namespace move_store_app.Controllers
{
    public class AuthenticationController : Controller
    {
        public readonly MoviesDbContext context;
        public AuthenticationController(MoviesDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult Login(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewBag.Error = errorMessage;
            }
            return View();
        }
        [HttpGet]
        public IActionResult Check(string Email, string Password)
        {
            var availACC = new Reg_Log_Repo(context);

            foreach (var email in availACC.GetAllReg())
            {
                if(Email == email.Email && Password == email.Password)
                {

                    HttpContext.Session.SetString("LoggedUserName", email.Name);

                    ViewBag.SuccessLogged = "Successfully Logged In!";

                    return RedirectToAction("Index", "Home",new {successLogged = ViewBag.SuccessLogged });
                }
            }
            foreach (var email in availACC.GetAllReg())
            {
                if(Email == email.Email)
                {
                    ViewBag.Error = "Invalid password!";
                    return RedirectToAction("Login", "Authentication", new { errorMessage = ViewBag.Error });
                }
            }
            foreach (var email in availACC.GetAllReg())
            {
                if (Email != email.Email && Password != email.Password)
                {
                    ViewBag.Error = "Invalid Data!";
                    return RedirectToAction("Login", "Authentication", new { errorMessage = ViewBag.Error });
                }
            }
            ViewBag.Error = "Invalid email!";
            return RedirectToAction("Login", "Authentication", new { errorMessage = ViewBag.Error });
        }

        [HttpGet]
        public IActionResult Sign_Up()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Sign_Up(string Name, string Email, string Password)
        {
            Registration_Login newReg = new Registration_Login();
            newReg.Name = Name;
            newReg.Email = Email;
            newReg.Password = Password;
            var availReg = new Reg_Log_Repo(context);
            foreach (var reg in availReg.GetAllReg())
            {
                if(newReg.Email == reg.Email)
                {
                    ViewBag.Error = "Account already exists!";
                    return View();
                }
            }
            availReg.AddReg(newReg);
            ViewBag.Success = "Account Created Successfully!";
            return RedirectToAction("Index", "Home", new { success = ViewBag.Success });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("LoggedUserName", "");
            ViewBag.successLoggedOut = "Successfully Logged Out!";
            return RedirectToAction("Index", "Home", new { successLoggedOut = ViewBag.successLoggedOut });
        }

    }
}
