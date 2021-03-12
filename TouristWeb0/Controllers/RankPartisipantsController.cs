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
    public class RankPartisipantsController : Controller
    {
        private readonly DBLab1Context _context;

        public RankPartisipantsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: RankPartisipants
        public async Task<IActionResult> Index()
        {
            var dBLab1Context = _context.RankPartisipants.Include(r => r.Partisipant).Include(r => r.Rank);
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: RankPartisipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankPartisipant = await _context.RankPartisipants
                .Include(r => r.Partisipant)
                .Include(r => r.Rank)
                .FirstOrDefaultAsync(m => m.RankPartisipantId == id);
            if (rankPartisipant == null)
            {
                return NotFound();
            }

            return View(rankPartisipant);
        }

        // GET: RankPartisipants/Create
        public IActionResult Create()
        {
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name");
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "Name");
            return View();
        }

        // POST: RankPartisipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RankPartisipantId,RankId,PartisipantId,DateOfAchievement")] RankPartisipant rankPartisipant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rankPartisipant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", rankPartisipant.PartisipantId);
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "Name", rankPartisipant.RankId);
            return View(rankPartisipant);
        }

        // GET: RankPartisipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankPartisipant = await _context.RankPartisipants.FindAsync(id);
            if (rankPartisipant == null)
            {
                return NotFound();
            }
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", rankPartisipant.PartisipantId);
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "Name", rankPartisipant.RankId);
            return View(rankPartisipant);
        }

        // POST: RankPartisipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RankPartisipantId,RankId,PartisipantId,DateOfAchievement")] RankPartisipant rankPartisipant)
        {
            if (id != rankPartisipant.RankPartisipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rankPartisipant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankPartisipantExists(rankPartisipant.RankPartisipantId))
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
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", rankPartisipant.PartisipantId);
            ViewData["RankId"] = new SelectList(_context.Ranks, "RankId", "Name", rankPartisipant.RankId);
            return View(rankPartisipant);
        }

        // GET: RankPartisipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rankPartisipant = await _context.RankPartisipants
                .Include(r => r.Partisipant)
                .Include(r => r.Rank)
                .FirstOrDefaultAsync(m => m.RankPartisipantId == id);
            if (rankPartisipant == null)
            {
                return NotFound();
            }

            return View(rankPartisipant);
        }

        // POST: RankPartisipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rankPartisipant = await _context.RankPartisipants.FindAsync(id);
            _context.RankPartisipants.Remove(rankPartisipant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankPartisipantExists(int id)
        {
            return _context.RankPartisipants.Any(e => e.RankPartisipantId == id);
        }
    }
}
