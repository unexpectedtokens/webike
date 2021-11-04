using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using webike.Models;
using webike.ViewModels;
using System.Linq;
namespace webike.Controllers
{
    public class HomeController : Controller
    {


        private WebikeContext _ctx {get;set;}

        public HomeController(WebikeContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {

            var @vm = new HomeViewModel();
            @vm.Events = (from b in _ctx.Events select b).Take(4).ToList();
            @vm.Workouts = (from b in _ctx.Workouts select b).Take(4).ToList();
            @vm.Routes = (from b in _ctx.Routes select b).Take(4).ToList();
            return View(@vm);
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Event()
        {
            return View();
        }
        public IActionResult Exercise()
        {
            return View();
        }
        public IActionResult Route()
        {
            return View();
        }
        public IActionResult Training()
        {
            return View();
        }
    }
}
