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
    public class vItem
    {
        public string Name { get; set; }
        public int CompetitionId { get; set; }
        public int ObstacleId { get; set; }
    }
    public class ObstacleCompetitionsController : Controller
    {
        private readonly DBLab1Context _context;

        public ObstacleCompetitionsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: ObstacleCompetitions
        public async Task<IActionResult> Index(int ?id,string name)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Competitions");
            }
            var dBLab1Context = _context.ObstacleCompetitions.Where(a=>a.CompetitionId==id).Include(o => o.Competition).Include(o => o.Obstacle);
            ViewBag.NameCompetition = name;
            ViewBag.CompId0 = id;
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: ObstacleCompetitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obstacleCompetition = await _context.ObstacleCompetitions
                .Include(o => o.Competition)
                .Include(o => o.Obstacle)
                .FirstOrDefaultAsync(m => m.ObstacleCompetitionId == id);
            if (obstacleCompetition == null)
            {
                return NotFound();
            }

            return RedirectToAction("Details","Obstacles",new {id=obstacleCompetition.ObstacleId });
        }
        public IActionResult Add(int ?id)
        {
            return RedirectToAction("Index", "Obstacles", new { id = id });
        }
        // GET: ObstacleCompetitions/Create
        
        public async Task<IActionResult> Create(int? idComp, int? idOb)
        {
           
            if (idComp != null && idOb != null) 
            {
                var comp =await _context.Competitions.FindAsync(idComp);
                var ob =await _context.Obstacles.FindAsync(idOb);
                if (ob == null) { return NotFound(); }
                if (comp == null) { return NotFound(); }
                ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", comp.CompetitionId);
                ViewData["ObstacleId"] = new SelectList(_context.Obstacles, "ObstacleId", "Name", ob.ObstacleId);
            }
            else
            {
                ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name");
                ViewData["ObstacleId"] = new SelectList(_context.Obstacles, "ObstacleId", "Name");
            }
            return View();
        }

        // POST: ObstacleCompetitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObstacleCompetitionId,ObstacleId,CompetitionId")] ObstacleCompetition obstacleCompetition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obstacleCompetition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", obstacleCompetition.CompetitionId);
            ViewData["ObstacleId"] = new SelectList(_context.Obstacles, "ObstacleId", "Name", obstacleCompetition.ObstacleId);
            return View(obstacleCompetition);
        }

        // GET: ObstacleCompetitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obstacleCompetition = await _context.ObstacleCompetitions.FindAsync(id);
            if (obstacleCompetition == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", obstacleCompetition.CompetitionId);
            ViewData["ObstacleId"] = new SelectList(_context.Obstacles, "ObstacleId", "Name", obstacleCompetition.ObstacleId);
            return View(obstacleCompetition);
        }

        // POST: ObstacleCompetitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObstacleCompetitionId,ObstacleId,CompetitionId")] ObstacleCompetition obstacleCompetition)
        {
            if (id != obstacleCompetition.ObstacleCompetitionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obstacleCompetition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObstacleCompetitionExists(obstacleCompetition.ObstacleCompetitionId))
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
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", obstacleCompetition.CompetitionId);
            ViewData["ObstacleId"] = new SelectList(_context.Obstacles, "ObstacleId", "Name", obstacleCompetition.ObstacleId);
            return View(obstacleCompetition);
        }

        // GET: ObstacleCompetitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obstacleCompetition = await _context.ObstacleCompetitions
                .Include(o => o.Competition)
                .Include(o => o.Obstacle)
                .FirstOrDefaultAsync(m => m.ObstacleCompetitionId == id);
            if (obstacleCompetition == null)
            {
                return NotFound();
            }

            return View(obstacleCompetition);
        }

        // POST: ObstacleCompetitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obstacleCompetition = await _context.ObstacleCompetitions.FindAsync(id);
            _context.ObstacleCompetitions.Remove(obstacleCompetition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObstacleCompetitionExists(int id)
        {
            return _context.ObstacleCompetitions.Any(e => e.ObstacleCompetitionId == id);
        }
    }
}
