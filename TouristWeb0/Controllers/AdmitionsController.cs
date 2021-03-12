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
    public class AdmitionsController : Controller
    {
        private readonly DBLab1Context _context;

        public AdmitionsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Admitions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admitions.ToListAsync());
        }

        // GET: Admitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admition = await _context.Admitions
                .FirstOrDefaultAsync(m => m.AdmittedId == id);
            if (admition == null)
            {
                return NotFound();
            }

            return View(admition);
        }

        // GET: Admitions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdmittedId,Name")] Admition admition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admition);
        }

        // GET: Admitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admition = await _context.Admitions.FindAsync(id);
            if (admition == null)
            {
                return NotFound();
            }
            return View(admition);
        }

        // POST: Admitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdmittedId,Name")] Admition admition)
        {
            if (id != admition.AdmittedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmitionExists(admition.AdmittedId))
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
            return View(admition);
        }

        // GET: Admitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admition = await _context.Admitions
                .FirstOrDefaultAsync(m => m.AdmittedId == id);
            if (admition == null)
            {
                return NotFound();
            }

            return View(admition);
        }

        // POST: Admitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admition = await _context.Admitions.FindAsync(id);
            _context.Admitions.Remove(admition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmitionExists(int id)
        {
            return _context.Admitions.Any(e => e.AdmittedId == id);
        }
    }
}
