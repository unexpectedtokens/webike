using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace webike.Models
{
    public class Event
    {

        public int EvetnID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string StartLocation { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Cyclist Maneger { get; set; }
        [Required]
        public List<Cyclist> Attendees { get; set; }
        [Required]
        public List<Rating> Ratings { get; set; }


   
    }
}