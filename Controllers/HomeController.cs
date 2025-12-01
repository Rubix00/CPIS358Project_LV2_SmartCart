using System.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using WebApplication_SMARTCART.Data;
using WebApplication_SMARTCART.Models;

namespace WebApplication_SMARTCART.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        // here i create a controller for each page 

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Demo()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Howitwork()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIN()
        {
            return View();
        }

        //  here in the log in it will check form the Db if the to see the first mathcing row of the same UserName
        // also the ris hte session stroin gthe username and the id 

        [HttpPost]
        public IActionResult LogIN(string UserName,string Password)
        {
         var check = _db.Customers.FirstOrDefault(u => u.UserName == UserName);
            if (check == null)
            {
                ViewBag.Error = "username not found";
                return View();
            }

            if (check.Password != Password)
            {
                ViewBag.Error = "Wrong Password";
                return View();
            }

            HttpContext.Session.SetString("UserName", check.UserName);
            HttpContext.Session.SetInt32("UserId", check.Id);
            return RedirectToAction("Home"); 
           
        }




        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // insert the model dta in  the Db 

        [HttpPost]
        public IActionResult SignUp(Customer model)
            
        {
            _db.Customers.Add(model);
            _db.SaveChanges();
            return RedirectToAction("LogIN");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIN");
        }

  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
