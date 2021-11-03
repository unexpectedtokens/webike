
using Microsoft.AspNetCore.Mvc;
using webike.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using webike.ViewModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
            List<int> toExcludeContactIDS = new List<int>(
                curCyc.UserID
            );

            vm.Contacts.ForEach(x=>{
                if(x.Accepted){
                    toExcludeContactIDS.Add(x.Sender.UserID);
                }
            });
            var contacts = _ctx.Cyclists.Include(c => c.Contacts).ThenInclude(c => c.Sender).Where(c => c.UserID != curCyc.UserID).ToList();
            foreach(var x in contacts)
            {
                var isContact = false;
                foreach(var c in x.Contacts)
                {
                    if(!isContact && c.Sender.UserID == curCyc.UserID){
                        isContact = true;
                        break;
                    }
                }
                if (isContact){
                    var newContact = new Contact();
                    newContact.Sender = x;
                    newContact.Accepted = true;
                    vm.Contacts.Add(newContact);
                }
                
                
            }
            vm.Cyclists = _ctx.Cyclists.Where(x => !toExcludeContactIDS.Contains(x.UserID)).ToList();;
            return View(vm);
        }


        public IActionResult Add(int id)
        {
            var senderid = HttpContext.Session.GetInt32("userid");
            if (senderid == null){
                return RedirectToAction("Index", "Auth");
            }
            if(id == 0){
                return RedirectToAction("Index", "Auth");
            }
            var curCyc = Cyclist.GetCyclistFromID(_ctx, Convert.ToInt32(senderid));
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