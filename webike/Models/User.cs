using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
namespace webike.Models
{

    public abstract class User
    {


        [Key]
        public int UserID {get;set;}
        public string Alias {get;set;}
        public string Password {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public DateTime DateOfBirth {get;set;}
        public string City {get;set;}
        public bool IsCoach {get;set;}
        public bool WantsToBeCoach {get;set;}
        //public string Events {get;set;}
        
        //public List<Contact> Contacts {get;set;}
        public void BecomeCoachRequest(WebikeContext ctx)
        {
            this.WantsToBeCoach = true;
            ctx.SaveChanges();
        }
    }
}