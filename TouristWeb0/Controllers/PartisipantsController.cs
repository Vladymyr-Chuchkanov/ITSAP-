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
    public class PartisipantsController : Controller
    {
        private readonly DBLab1Context _context;

        public PartisipantsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Partisipants
        public async Task<IActionResult> Index()
        {
            var dBLab1Context = _context.Partisipants.Include(p => p.IdRoleNavigation).Include(p => p.IdSexNavigation);
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: Partisipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partisipant = await _context.Partisipants
                .Include(p => p.IdRoleNavigation)
                .Include(p => p.IdSexNavigation)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (partisipant == null)
            {
                return NotFound();
            }

            return View(partisipant);
        }

        // GET: Partisipants/Create
        public IActionResult Create()
        {
            ViewData["IdRole"] = new SelectList(_context.Roles, "RolesId", "Name");
            ViewData["IdSex"] = new SelectList(_context.Sexes, "SexId", "Name");
            return View();
        }

        // POST: Partisipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticipantId,Name,DateOfBirth,IdSex,PhoneNumber,IdRole,FileInsurance")] Partisipant partisipant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partisipant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "RolesId", "Name", partisipant.IdRole);
            ViewData["IdSex"] = new SelectList(_context.Sexes, "SexId", "Name", partisipant.IdSex);
            return View(partisipant);
        }

        // GET: Partisipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partisipant = await _context.Partisipants.FindAsync(id);
            if (partisipant == null)
            {
                return NotFound();
            }
            ViewData["IdRole"] = new SelectList(_context.Roles, "RolesId", "Name", partisipant.IdRole);
            ViewData["IdSex"] = new SelectList(_context.Sexes, "SexId", "Name", partisipant.IdSex);
            return View(partisipant);
        }

        // POST: Partisipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticipantId,Name,DateOfBirth,IdSex,PhoneNumber,IdRole,FileInsurance")] Partisipant partisipant)
        {
            if (id != partisipant.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partisipant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartisipantExists(partisipant.ParticipantId))
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
            ViewData["IdRole"] = new SelectList(_context.Roles, "RolesId", "Name", partisipant.IdRole);
            ViewData["IdSex"] = new SelectList(_context.Sexes, "SexId", "Name", partisipant.IdSex);
            return View(partisipant);
        }

        // GET: Partisipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partisipant = await _context.Partisipants
                .Include(p => p.IdRoleNavigation)
                .Include(p => p.IdSexNavigation)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (partisipant == null)
            {
                return NotFound();
            }

            return View(partisipant);
        }

        // POST: Partisipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partisipant = await _context.Partisipants.FindAsync(id);
            _context.Partisipants.Remove(partisipant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartisipantExists(int id)
        {
            return _context.Partisipants.Any(e => e.ParticipantId == id);
        }
    }
}
