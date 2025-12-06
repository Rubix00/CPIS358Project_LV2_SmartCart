using Microsoft.AspNetCore.Mvc;
using WebApplication_SMARTCART.Data;
using WebApplication_SMARTCART.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication_SMARTCART.Controllers
{
    public class HomeController : Controller
    {

        // variable stor the DB  to access the cusotmer table

        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
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


        public IActionResult Howitwork()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }












        // this for showing the signup form only

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        //  reciving and prcess signup form
        [HttpPost]
        public IActionResult SignUp(Customer customer, string confirm)
        {
            if (customer.Password != confirm)
            {
                ViewBag.Error = "Password do not match ";
                return View(customer);
            }

            if (customer.UserName.Length < 5)
            {
                ViewBag.Error = "Username must be at leat 5 charcters ";
                return View(customer);

            }
            // add to the DB
            _context.Customers.Add(customer);
            // save the changes

            _context.SaveChanges();

            // redirect the user to teh login page after sign up 

            return RedirectToAction("LogIN");

        }
        [HttpGet]
        public IActionResult LogIN()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIN(string UserName, string password)
        {
            // search for the user in DB 
            var MatchUser = _context.Customers.FirstOrDefault(c => c.UserName == UserName && c.Password == password);
            if (MatchUser == null)
            {
                ViewBag.Error = "invalid usernaem or password";


                return View();
                

            }
            //Retrive the USerName and save it in Session 
            HttpContext.Session.SetString("UserName", MatchUser.UserName);

            // Save cookies
            CookieOptions cooke = new CookieOptions();
            cooke.Expires = DateTime.Now.AddDays(2);

            Response.Cookies.Append("UserName", MatchUser.UserName, cooke); 

            return RedirectToAction("Home");
        }

        public IActionResult Logout()
        {
            // delete seeion 
            HttpContext.Session.Clear();

            // Delet cookies
            Response.Cookies.Delete("UserName"); 
            return RedirectToAction("Index");
        }
    }
}