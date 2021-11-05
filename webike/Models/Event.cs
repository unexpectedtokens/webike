using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace webike.Models
{
    public class Event
    {

        [Key]
        public int EventID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string StartLocation { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("Manager")]

        public int? ManagerID {get;set;}
        public Cyclist Manager { get; set; }
        public List<Cyclist> Attendees { get; set; }
        public List<Rating> Ratings { get; set; }
        public bool UserIsOwner(string alias)
        {
            return alias == Manager.Alias;
        }
        public bool Public {get;set;}

        public EventActivity Activity {get;set;}
   
    }
}