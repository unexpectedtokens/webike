using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webike.Models
{
    public abstract class EventActivity
    {
        [Key]
        public int EventActivityID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Cyclist Creator { get; set; }
        [Required]
        public List<Rating> Ratings { get; set; }
        [Required]
        public Difficulty Difficulty { get; set; }
        [Required]
        public BikeType SuitableBikeType { get; set; }

        public List<Event> Events {get;set;}
    }
}