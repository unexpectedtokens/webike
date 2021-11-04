using System.Collections.Generic;
using webike.Models;

namespace webike.ViewModels
{
    public class HomeViewModel
    {

        public ICollection<Event> Events {get;set;}
        public ICollection<Workout> Workouts {get;set;}

        public ICollection<Route> Routes {get;set;}
    }
}