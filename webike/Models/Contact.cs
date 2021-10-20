
using System.ComponentModel.DataAnnotations;


namespace webike.Models
{
    public class Contact
    {
        public User Sender {get;set;}
        [Required]
        public bool Accepted {get;set;}
    }
}