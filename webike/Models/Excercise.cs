using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace webike.Models
{
    public class Excercise 
    {
        [Key]
        public int ExcerciseID {get;set;}
        public Cyclist Creator {get;set;}
        public string Description {get;set;}
        public string Title {get;set;}

        public ICollection<Workout> WorkoutsPartOf {get;set;}
    }
}