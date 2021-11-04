
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
            vm.Contacts = new List<Contact>();
            vm.PendingContacts = new List<Contact>();
            
            List<int> toExcludeContactIDS = new List<int>{
                curCyc.UserID,
            };
            var contacts = _ctx.Contacts.Include(c => c.Sender).Include(c => c.Receiver).Where(c => c.Sender.UserID == curCyc.UserID || c.Receiver.UserID == curCyc.UserID).ToList();
            
            
            contacts.ForEach(x=>{                
                 toExcludeContactIDS.Add(x.Sender.UserID);  
                 Console.WriteLine(x.Sender.Alias);
                 Console.WriteLine(x.Receiver.Alias);
                 if(x.Accepted){
                     vm.Contacts.Add(x);
                 }else{
                     if(x.Receiver.UserID == curCyc.UserID)
                     {
                         vm.PendingContacts.Add(x);
                     }
                 }              
            });
            // var contacts = _ctx.Cyclists.Include(c => c.Contacts).ThenInclude(c => c.Sender).Where(c => c.UserID != curCyc.UserID).ToList();
            // foreach(var x in contacts)
            // {
            //     var isContact = false;
            //     var isPending = false;
            //     foreach(var c in x.Contacts)
            //     {
            //         if(c.Sender.UserID == curCyc.UserID){
            //             if(c.Accepted){
            //                 isContact = true;
            //             }else{
            //                 isPending = true;
            //             }
            //             break;
            //         }
            //     }
            //     if(isContact || isPending)
            //     {
            //         toExcludeContactIDS.Add(x.UserID);
            //         var newContact = new Contact();
            //         newContact.Sender = x;
            //         if(isContact)
            //         {
            //             newContact.Accepted = true;
            //             vm.Contacts.Add(newContact);

            //         }
            //         if(isPending)
            //         {
            //             newContact.Accepted = false;
                        
            //             vm.PendingContacts.Add(newContact);
            //         }    
            //     }
            // }                
            // foreach(var item in toExcludeContactIDS)
            // {
            //     Console.WriteLine(item);
            // }
            vm.Cyclists = _ctx.Cyclists.Where(x => !toExcludeContactIDS.Contains(x.UserID)).ToList();
            vm.CurUserName = curCyc.Alias;
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
            newContact.Receiver = receivingCyc;
            _ctx.Contacts.Add(newContact);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }


        public IActionResult Accept(int id)
        {
            var contact = _ctx.Contacts.FirstOrDefault(c => c.ContactID == id);
            if(contact == null)
            {
                return NotFound();
            }
            contact.Accepted = true;
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }

        public IActionResult Reject(int id)
        {   
            var contact = _ctx.Contacts.FirstOrDefault(c => c.ContactID == id);
            if(contact == null)
            {
                return NotFound();
            }
            _ctx.Contacts.Remove(contact);
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }

    }



}