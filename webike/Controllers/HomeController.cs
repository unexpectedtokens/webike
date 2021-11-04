﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using webike.Models;
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
            return View();
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
