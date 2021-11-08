using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webike.Models;

namespace webike.Controllers
{
    public class AdminController : Controller
    {
        private readonly WebikeContext _context;

        public AdminController(WebikeContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cyclists.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cyclist = await _context.Cyclists
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (cyclist == null)
            {
                return NotFound();
            }

            return View(cyclist);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Alias,Password,FirstName,LastName,DateOfBirth,City,IsCoach,WantsToBeCoach")] Cyclist cyclist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cyclist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cyclist);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cyclist = await _context.Cyclists.FindAsync(id);
            if (cyclist == null)
            {
                return NotFound();
            }
            return View(cyclist);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Alias,Password,FirstName,LastName,DateOfBirth,City,IsCoach,WantsToBeCoach")] Cyclist cyclist)
        {
            if (id != cyclist.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cyclist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CyclistExists(cyclist.UserID))
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
            return View(cyclist);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cyclist = await _context.Cyclists
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (cyclist == null)
            {
                return NotFound();
            }

            return View(cyclist);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cyclist = await _context.Cyclists.FindAsync(id);
            _context.Cyclists.Remove(cyclist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CyclistExists(int id)
        {
            return _context.Cyclists.Any(e => e.UserID == id);
        }
        public IActionResult AcceptBecomeCoachRequest(int id)
        {
            var user = _context.Cyclists.FirstOrDefault(c => c.UserID == id);

            user.IsCoach = true;
            user.WantsToBeCoach = false;
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult DenyBecomeCoachRequest(int id)
        {
            var user = _context.Cyclists.FirstOrDefault(c => c.UserID == id);

            user.WantsToBeCoach = false;
            _context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}
