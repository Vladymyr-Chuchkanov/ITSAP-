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
    public class CompetitionsController : Controller
    {
        private readonly DBLab1Context _context;

        public CompetitionsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var dBLab1Context = _context.Competitions.Include(c => c.IdComplexityNavigation).Include(c => c.IdTypeNavigation);
            var CurrentDate =  DateTime.Now;
            ViewBag.Date0 = CurrentDate;
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions
                .Include(c => c.IdComplexityNavigation)
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.CompetitionId == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }
        public async Task<IActionResult> Obstacles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions
                .Include(c => c.IdComplexityNavigation)
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.CompetitionId == id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewBag.CompId0 = competition.CompetitionId;
            return RedirectToAction("Index", "ObstacleCompetitions", new { id = competition.CompetitionId, name = competition.Name });
        }
        public async Task<IActionResult> Applications(int id)
        {

            var ids0 = await _context.CompetitionTeams.ToListAsync();
            var X = await _context.Competitions.FindAsync(id);
            string name = X.Name;
            List<int> ids = new List<int>();
            foreach(var it in ids0)
            {
                if (it.CompetitionId == id) { 
                    
                    ids.Add(it.TeamId);
                }
            }

            return RedirectToAction("Index", "Teams", new { lst = ids,id = id, name = name });
        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            ViewData["IdComplexity"] = new SelectList(_context.Complexities, "ComplexityId", "Name");
            ViewData["IdType"] = new SelectList(_context.Types, "TypeId", "Name");
            return View();
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionId,Name,Place,StartTax,StartTime,IdComplexity,IdType")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdComplexity"] = new SelectList(_context.Complexities, "ComplexityId", "Name", competition.IdComplexity);
            ViewData["IdType"] = new SelectList(_context.Types, "TypeId", "Name", competition.IdType);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewData["IdComplexity"] = new SelectList(_context.Complexities, "ComplexityId", "Name", competition.IdComplexity);
            ViewData["IdType"] = new SelectList(_context.Types, "TypeId", "Name", competition.IdType);
            return View(competition);
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetitionId,Name,Place,StartTax,StartTime,IdComplexity,IdType")] Competition competition)
        {
            if (id != competition.CompetitionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.CompetitionId))
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
            ViewData["IdComplexity"] = new SelectList(_context.Complexities, "ComplexityId", "Name", competition.IdComplexity);
            ViewData["IdType"] = new SelectList(_context.Types, "TypeId", "Name", competition.IdType);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions
                .Include(c => c.IdComplexityNavigation)
                .Include(c => c.IdTypeNavigation)
                .FirstOrDefaultAsync(m => m.CompetitionId == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.CompetitionId == id);
        }
    }
}
