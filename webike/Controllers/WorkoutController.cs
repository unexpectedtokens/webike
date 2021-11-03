using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webike.Models;
using webike.ViewModels;
namespace webike.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly WebikeContext _context;

        public WorkoutController(WebikeContext context)
        {
            _context = context;
        }

        // GET: Workout
        public async Task<IActionResult> Index()
        {
            return View(await _context.Workouts.ToListAsync());
        }

        // GET: Workout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts.Include(w => w.Ratings).ThenInclude(r => r.Cyclist)
                .FirstOrDefaultAsync(m => m.EventActivityID == id);
            if (workout == null)
            {
                return NotFound();
            }
            var @vm = new WorkoutViewModel();
            @vm.Workout = workout;
            return View(@vm);
        }

        // GET: Workout/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventActivityID,Title,Difficulty,SuitableBikeType")] Workout workout)
        {
            _context.Add(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Workout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workout/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventActivityID,Title,Difficulty,SuitableBikeType")] Workout workout)
        {
            if (id != workout.EventActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.EventActivityID))
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
            return View(workout);
        }

        // GET: Workout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .FirstOrDefaultAsync(m => m.EventActivityID == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> CreateRating(int? id, WorkoutViewModel workoutViewModel){
            if(id == null){
                return Redirect(nameof(Index));
            }
            var curUserID = HttpContext.Session.GetInt32("userid");
            if(curUserID == null){
                return RedirectToAction("Index", "Auth");
            }
            var curUser = await _context.Cyclists.FirstOrDefaultAsync(c => c.UserID == curUserID);
            var wo = await _context.Workouts.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.EventActivityID == id);
            Console.WriteLine(wo.EventActivityID);
            workoutViewModel.NewRating.Cyclist = curUser;
            wo.Ratings.Add(workoutViewModel.NewRating);
            _context.SaveChanges();
            return Redirect($"/Workout/Details/{id}");
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.EventActivityID == id);
        }
    }
}
