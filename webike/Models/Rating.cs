
using System.ComponentModel.DataAnnotations;


namespace webike.Models
{

    public class Rating
    {
        [Key]
        public int RatingID {get;set;}
        public int Number {get;set;}
        public string Description {get;set;}
        public Cyclist Cyclist {get;set;}

        public void SetRatingNumber(int x)
        {
            //check of rating number tussen 1 en 5 zit
            //zet nummer
        }
    }

}