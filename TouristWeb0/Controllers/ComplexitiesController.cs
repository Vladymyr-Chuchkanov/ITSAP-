using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TouristWeb0;

namespace TouristWeb0.Controllers
{
    public class ComplexitiesController : Controller
    {
        private readonly DBLab1Context _context;

        public ComplexitiesController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Complexities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Complexities.ToListAsync());
        }

        // GET: Complexities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complexity = await _context.Complexities
                .FirstOrDefaultAsync(m => m.ComplexityId == id);
            if (complexity == null)
            {
                return NotFound();
            }

            return View(complexity);
        }

        // GET: Complexities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Complexities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplexityId,Name")] Complexity complexity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complexity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complexity);
        }

        // GET: Complexities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complexity = await _context.Complexities.FindAsync(id);
            if (complexity == null)
            {
                return NotFound();
            }
            return View(complexity);
        }

        // POST: Complexities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplexityId,Name")] Complexity complexity)
        {
            if (id != complexity.ComplexityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complexity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplexityExists(complexity.ComplexityId))
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
            return View(complexity);
        }

        // GET: Complexities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complexity = await _context.Complexities
                .FirstOrDefaultAsync(m => m.ComplexityId == id);
            if (complexity == null)
            {
                return NotFound();
            }

            return View(complexity);
        }

        // POST: Complexities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complexity = await _context.Complexities.FindAsync(id);
            _context.Complexities.Remove(complexity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplexityExists(int id)
        {
            return _context.Complexities.Any(e => e.ComplexityId == id);
        }
    }
}
