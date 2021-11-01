

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using webike.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
namespace webike.Controllers
{
    public class AuthController : Controller
    {

        public WebikeContext _ctx {get;set;}

        public AuthController(WebikeContext ctx)
        {
            _ctx = ctx;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Cyclist c)
        {
            var cfdb = _ctx.Cyclists.Where(u => u.Alias == c.Alias).FirstOrDefault();
            if(cfdb == null){
                Console.WriteLine("un unknown");
                return Index();
            }
            if(cfdb.Password != c.Password){
                Console.WriteLine("pw wrong");
                return Index();
            }
            HttpContext.Session.SetInt32("userid", cfdb.UserID);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register ()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register (Cyclist c)
        {
            //Input valideren

            //

            _ctx.Cyclists.Add(c);
            _ctx.SaveChanges();
            HttpContext.Session.SetInt32("userid", c.UserID);
            return RedirectToAction("Index", "Home");
        }


    }
}