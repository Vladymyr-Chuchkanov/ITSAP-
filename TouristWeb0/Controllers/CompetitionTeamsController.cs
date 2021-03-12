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
    public class CompetitionTeamsController : Controller
    {
        private readonly DBLab1Context _context;

        public CompetitionTeamsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: CompetitionTeams
        public async Task<IActionResult> Index()
        {
            var dBLab1Context = _context.CompetitionTeams.Include(c => c.Admitted).Include(c => c.Competition).Include(c => c.Team);
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: CompetitionTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionTeam = await _context.CompetitionTeams
                .Include(c => c.Admitted)
                .Include(c => c.Competition)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.CompetitionTeamId == id);
            if (competitionTeam == null)
            {
                return NotFound();
            }

            return View(competitionTeam);
        }

        // GET: CompetitionTeams/Create
        
        public async Task<IActionResult> Create(int? id, int? id1)
        {
            ViewData["AdmittedId"] = new SelectList(_context.Admitions, "AdmittedId", "Name");
            if (id != null && id1 != null)
            {
                var comp = await _context.Competitions.FindAsync(id);
                var team = await _context.Teams.FindAsync(id1);
                if (team == null) { return NotFound(); }
                if (comp == null) { return NotFound(); }
                ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", comp.CompetitionId);
                ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", team.TeamId);
            }
            else
            {
                ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name");
                ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name");
            }
            return View();
        }
        // POST: CompetitionTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompetitionTeamId,CompetitionId,TeamId,AdmittedId,ResultTime,Penalty,Position")] CompetitionTeam competitionTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competitionTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdmittedId"] = new SelectList(_context.Admitions, "AdmittedId", "Name", competitionTeam.AdmittedId);
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", competitionTeam.CompetitionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Name", competitionTeam.TeamId);
            return View(competitionTeam);
        }

        // GET: CompetitionTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionTeam = await _context.CompetitionTeams.FindAsync(id);
            if (competitionTeam == null)
            {
                return NotFound();
            }
            ViewData["AdmittedId"] = new SelectList(_context.Admitions, "AdmittedId", "Name", competitionTeam.AdmittedId);
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", competitionTeam.CompetitionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Comment", competitionTeam.TeamId);
            return View(competitionTeam);
        }

        // POST: CompetitionTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompetitionTeamId,CompetitionId,TeamId,AdmittedId,ResultTime,Penalty,Position")] CompetitionTeam competitionTeam)
        {
            if (id != competitionTeam.CompetitionTeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competitionTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionTeamExists(competitionTeam.CompetitionTeamId))
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
            ViewData["AdmittedId"] = new SelectList(_context.Admitions, "AdmittedId", "Name", competitionTeam.AdmittedId);
            ViewData["CompetitionId"] = new SelectList(_context.Competitions, "CompetitionId", "Name", competitionTeam.CompetitionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Comment", competitionTeam.TeamId);
            return View(competitionTeam);
        }

        // GET: CompetitionTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competitionTeam = await _context.CompetitionTeams
                .Include(c => c.Admitted)
                .Include(c => c.Competition)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.CompetitionTeamId == id);
            if (competitionTeam == null)
            {
                return NotFound();
            }

            return View(competitionTeam);
        }

        // POST: CompetitionTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competitionTeam = await _context.CompetitionTeams.FindAsync(id);
            _context.CompetitionTeams.Remove(competitionTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionTeamExists(int id)
        {
            return _context.CompetitionTeams.Any(e => e.CompetitionTeamId == id);
        }
    }
}
