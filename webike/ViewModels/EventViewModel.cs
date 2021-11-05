using webike.Models;
using System.Collections.Generic;

namespace webike.ViewModels
{
    public class EventViewModel{

        public Event Event {get;set;}
        public List<Rating> Ratings {get;set;}
        public List<Contact> Contacts { get; set; }

        public int SelectedEventActivityID {get;set;}

        public string CurUserAlias {get;set;}
        public bool ActivityIsWorkout {get;set;}
        public List<Route> PotenRoutes {get;set;}
        public List<Workout> PotenWorkouts {get;set;}

    }
}