using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webike.ViewModels;
using Microsoft.EntityFrameworkCore;
using webike.Models;

namespace webike.Controllers
{
    public class RouteController : Controller
    {
        private readonly WebikeContext _context;

        public RouteController(WebikeContext context)
        {
            _context = context;
        }

        // GET: Route
        public async Task<IActionResult> Index()
        {
            return View(await _context.Routes.ToListAsync());
        }

        // GET: Route/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.Include(e => e.Ratings).ThenInclude(r => r.Cyclist)
                .FirstOrDefaultAsync(m => m.EventActivityID == id);
            if (route == null)
            {
                return NotFound();
            }
            var @vm = new RouteViewModel();
            @vm.Route = route;
            return View(@vm);
        }

        // GET: Route/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Route/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartPoint,EndPoint,RouteDifficulty,Duration,Addition,EventActivityID,Title,Difficulty,SuitableBikeType")] Route route)
        {
            _context.Add(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }

        // GET: Route/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // POST: Route/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartPoint,EndPoint,RouteDifficulty,Duration,Addition,EventActivityID,Title,Difficulty,SuitableBikeType")] Route route)
        {
            if (id != route.EventActivityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.EventActivityID))
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
            return View(route);
        }

        // GET: Route/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .FirstOrDefaultAsync(m => m.EventActivityID == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(int? id, RouteViewModel routeViewModel){
            if(id == null){
                return Redirect(nameof(Index));
            }
            var curUserID = HttpContext.Session.GetInt32("userid");
            if(curUserID == null){
                return RedirectToAction("Index", "Auth");
            }
            var curUser = await _context.Cyclists.FirstOrDefaultAsync(c => c.UserID == curUserID);
            var route = await _context.Routes.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.EventActivityID == id);
            Console.WriteLine(route.EventActivityID);
            routeViewModel.NewRating.Cyclist = curUser;
            route.Ratings.Add(routeViewModel.NewRating);
            _context.SaveChanges();
            return Redirect($"/Route/Details/{id}");
        }

        // POST: Route/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.EventActivityID == id);
        }
    }
}
