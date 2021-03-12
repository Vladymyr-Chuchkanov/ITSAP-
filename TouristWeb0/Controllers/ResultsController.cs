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
    public class ResultsController : Controller
    {
        private readonly DBLab1Context _context;

        public ResultsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var dBLab1Context = _context.Results.Include(r => r.ObstacleCompetition).Include(r => r.Partisipant);
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.ObstacleCompetition)
                .Include(r => r.Partisipant)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            ViewData["ObstacleCompetitionId"] = new SelectList(_context.ObstacleCompetitions, "ObstacleCompetitionId", "ObstacleCompetitionId");
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,PartisipantId,ObstacleCompetitionId,Time,Penalty")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ObstacleCompetitionId"] = new SelectList(_context.ObstacleCompetitions, "ObstacleCompetitionId", "ObstacleCompetitionId", result.ObstacleCompetitionId);
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", result.PartisipantId);
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["ObstacleCompetitionId"] = new SelectList(_context.ObstacleCompetitions, "ObstacleCompetitionId", "ObstacleCompetitionId", result.ObstacleCompetitionId);
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", result.PartisipantId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,PartisipantId,ObstacleCompetitionId,Time,Penalty")] Result result)
        {
            if (id != result.ResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.ResultId))
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
            ViewData["ObstacleCompetitionId"] = new SelectList(_context.ObstacleCompetitions, "ObstacleCompetitionId", "ObstacleCompetitionId", result.ObstacleCompetitionId);
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", result.PartisipantId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.ObstacleCompetition)
                .Include(r => r.Partisipant)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.FindAsync(id);
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.ResultId == id);
        }
    }
}
