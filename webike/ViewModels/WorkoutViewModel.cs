using webike.Models;
using System.Collections.Generic;


namespace webike.ViewModels
{
    public class WorkoutViewModel{
        public Workout Workout {get;set;}
        public Rating NewRating {get;set;}

        public bool UserIsOwner {get;set;}

        public List<Excercise> ExcercisesToAdd {get;set;}
    }
}