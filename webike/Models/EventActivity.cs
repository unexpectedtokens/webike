using System;
using System.Collections.Concurrent;
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
        public Rating Ratings { get; set; }
        [Required]
        public Difficulty Difficulty { get; set; }
        [Required]
        public BikeType SuitableBikeType { get; set; }
    }
}