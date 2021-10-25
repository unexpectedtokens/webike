
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace webike.Models
{

    public class Workout : EventActivity
    {
        public List<Excercise> Excercises {get;set;}
    }

}