
using Microsoft.AspNetCore.Mvc;
using webike.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using webike.ViewModels;


namespace webike.Controllers
{
    public class ContactController : Controller
    {
        public WebikeContext _ctx {get;set;}
        public ContactController(WebikeContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index()
        {
            var id = Convert.ToInt32(HttpContext.Session.GetInt32("userid"));
            if(id == 0){
                return RedirectToAction("Index", "Auth");
            }
            var curCyc = Cyclist.GetCyclistFromID(_ctx, id);
            var vm = new ContactViewModel();
            vm.Contacts = curCyc.Contacts;
            vm.Cyclists = _ctx.Cyclists.Where(x => true).ToList();;
            return View(vm);
        }


        [HttpPost]
        public IActionResult Add(int id)
        {
            var senderid = Convert.ToInt32(HttpContext.Session.GetInt32("userid"));
            if(id == 0){
                return RedirectToAction("Index", "Auth");
            }
            var curCyc = Cyclist.GetCyclistFromID(_ctx, senderid);
            var receivingCyc = Cyclist.GetCyclistFromID(_ctx, id);
            var newContact = new Contact();
            newContact.Accepted = false;
            newContact.Sender = curCyc;
            receivingCyc.Contacts.Add(newContact);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }


    }



}