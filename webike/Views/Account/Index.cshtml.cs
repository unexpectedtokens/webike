using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webike.Models;

namespace webike.Views.Account
{
    public class IndexModel : PageModel
    {
        private readonly webike.Models.WebikeContext _context;

        public IndexModel(webike.Models.WebikeContext context)
        {
            _context = context;
        }

        public Cyclist Cyclist { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cyclist = await _context.Cyclists.FirstOrDefaultAsync(m => m.UserID == id);

            if (Cyclist == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
