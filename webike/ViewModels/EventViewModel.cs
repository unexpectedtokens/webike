using webike.Models;
using System.Collections.Generic;

namespace webike.ViewModels
{
    public class EventViewModel{

        public Event Event {get;set;}
        public List<Rating> Ratings {get;set;}
        public List<Contact> Contacts { get; set; }

        public int SelectedEventActivityID {get;set;}


    }
}