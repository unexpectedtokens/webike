using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webike.Models
{
    public class Route : EventActivity
    {
        public int RouteID { get; set;}
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public Difficulty RouteDifficulty { get; set; }
        public string Duration { get; set; }
        public string Addition { get; set; }
    }
}