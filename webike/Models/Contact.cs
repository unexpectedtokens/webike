
using System.ComponentModel.DataAnnotations;


namespace webike.Models
{
    public class Contact
    {
        [Key]
        public int ContactID {get;set;}

        public User Receiver {get;set;}

        
        public User Sender {get;set;}
        public bool Accepted {get;set;}


    }
}