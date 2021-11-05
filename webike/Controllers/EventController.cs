using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webike.ViewModels;
using Microsoft.EntityFrameworkCore;
using webike.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var uid = Convert.ToInt32(HttpContext.Session.GetInt32("userid"));
            if(uid == 0){
                return RedirectToAction("Index", "Auth");
            }
            var curCyc = Cyclist.GetCyclistFromID(_context, uid);
            

            var @event = await _context.Events.Include(e => e.Ratings).ThenInclude(r => r.Cyclist).Include(r => r.Attendees).Include(e => e.Manager)
                .FirstOrDefaultAsync(m => m.EventID == id);
            if (@event == null)
            {
                return NotFound();
            }
            var @vm = new EventViewModel();
            @vm.Event = @event;
            @vm.CurUserAlias = curCyc.Alias;
            if(@event.Activity == null)
            {
                @vm.PotenWorkouts = await _context.Workouts.ToListAsync();
                @vm.PotenRoutes = await _context.Routes.ToListAsync();
            }
            return View(@vm);
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
            if(uid == 0){
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



        //<a href="/Event/AddAct/@Model.Event.EventID?actid=@item.EventActivityID&typeoa=workout">Voeg toe aan dit event</a>
        public async Task<IActionResult> AddAct(int? id, int actid, string typeoa)
        {
            if(id == null || actid == 0)
            {
                return Redirect("/Event");
            }

            
            var e = await _context.Events.FirstOrDefaultAsync(e => e.EventID == id);
            if(typeoa == "workout")
            {
                var wo = await _context.Workouts.FirstOrDefaultAsync(w => w.EventActivityID == actid);
                e.Activity = wo;
            }else if(typeoa == "route"){
                var route = await _context.Routes.FirstOrDefaultAsync(w => w.EventActivityID == actid);
                e.Activity = route;
            }
            _context.SaveChanges();
            return Redirect($"/Event/Details/{id}");
        }


        public async Task<IActionResult> RemoveAct(int? id)
        {
            if(id == null)
            {
                return Redirect("/Event");
            }
            var e = await _context.Events.Include(e => e.Activity).FirstOrDefaultAsync(e => e.EventID == id);
            e.Activity = null;
            _context.SaveChanges();
            return Redirect($"/Event/Details/{id}");
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

        public IActionResult Join(int id)
        {
            var events = _context.Events.Find(id);
            ViewBag.Users = new SelectList(_context.Cyclists, "UserID", "Alias");
            JoinViewModel join = new();
            join.EventID = events.EventID;
            return View(join);
        }

        [HttpPost]
        public IActionResult JoinEvent(JoinViewModel jvm)
        {
            var @event = _context.Events.Where(g => g.EventID == jvm.EventID).Include(g => g.Attendees).Include(e => e.Ratings).FirstOrDefault();
            var user = _context.Cyclists.Find(jvm.UserID);
            @event.Attendees.Add(user);
            _context.SaveChanges();

            return View("Details", @event);
        }
    }
}
