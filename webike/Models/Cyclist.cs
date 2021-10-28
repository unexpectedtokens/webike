using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace webike.Models
{
    public class Cyclist : User
    {
        


        public static Cyclist GetCyclistFromID (WebikeContext ctx, int id)
        {
            return ctx.Cyclists.Include(c => c.Contacts).Where(c => c.UserID == id).FirstOrDefault();
        }
    }
}