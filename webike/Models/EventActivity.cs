using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace webike.Models
{
    public class EventActivity
    {
        public int EventActivityID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Cyclist Creatot { get; set; }
        [Required]
        public Rating Ratings { get; set; }
        [Required]
        public string Difficulty { get; set; }
        [Required]
        public BikeType SuitableBikeType { get; set; }
    }
}