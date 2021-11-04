using System.Linq;
using System.Collections.Generic;

namespace webike.Models
{
    public class Cyclist : User
    {
        
        public ICollection<Event> EventsPartOf {get;set;}

        public ICollection<Event> EventManaging {get;set;}

        public static Cyclist GetCyclistFromID (WebikeContext ctx, int id)
        {
            return ctx.Cyclists.Where(c => c.UserID == id).FirstOrDefault();
        }
    }
}