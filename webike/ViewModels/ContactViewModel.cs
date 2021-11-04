
using System.Collections.Generic;
using webike.Models;


namespace webike.ViewModels{

    public class ContactViewModel
    {
        public List<Cyclist> Cyclists {get;set;}
        public List<Contact> Contacts {get;set;}

        public List<Contact> PendingContacts {get;set;}
        public string CurUserName {get;set;}
    }

}