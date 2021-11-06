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
    public class AccountController : Controller
    {
        private readonly WebikeContext _context;

        public AccountController(WebikeContext context)
        {
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cyclists.ToListAsync());
        }

        // GET: Account/Details/5
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

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
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

        // GET: Account/Edit/5
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

        // POST: Account/Edit/5
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

        // GET: Account/Delete/5
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

        // POST: Account/Delete/5
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
    }
}
