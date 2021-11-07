using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webike.Models;
using webike.ViewModels;

namespace webike.Controllers
{
    public class EventController : Controller
    {
        private readonly WebikeContext _context;

        public EventController(WebikeContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.Include(e => e.Ratings).ThenInclude(r => r.Cyclist)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }
            
            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventID,Title,Date,StartLocation,Description,Public")] Event @event)
        {
            var uid = Convert.ToInt32(HttpContext.Session.GetInt32("userid"));
            if (uid == 0)
            {
                return RedirectToAction("Index", "Auth");
            }
            var curCyc = Cyclist.GetCyclistFromID(_context, uid);
            @event.Manager = curCyc;
            _context.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventID,Title,Date,StartLocation,Description,Public")] Event @event)
        {
            if (id != @event.EventID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(int? id)
        {
            // if(id == null){
            //     return NotFound()
            // }
            // var 
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(int? id, EventViewModel eventViewModel)
        {
            if (id == null)
            {
                return Redirect(nameof(Index));
            }
            var curUserID = HttpContext.Session.GetInt32("userid");
            if (curUserID == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            var curUser = await _context.Cyclists.FirstOrDefaultAsync(c => c.UserID == curUserID);
            var @event = await _context.Events.Include(r => r.Ratings). Include (r => r.Manager).FirstOrDefaultAsync(r => r.EventID == id);
            Console.WriteLine(@event.EventID);
            eventViewModel.NewRating.Cyclist = curUser;
            @event.Ratings.Add(eventViewModel.NewRating);
            _context.SaveChanges();
            return Redirect($"/Event/Details/{id}");
        }
    }
}
