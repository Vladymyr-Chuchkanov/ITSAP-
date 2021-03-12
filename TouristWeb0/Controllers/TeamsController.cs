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
    public class TeamsController : Controller
    {
        private readonly DBLab1Context _context;

        public TeamsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index(List<int> lst,int id,string name)
        {
            ViewBag.CompId0 = id;
            ViewBag.Name0 = name;
            
            var Z = await _context.Teams.ToListAsync();
            Z.Clear();
            if (lst == null)
            {
                return View(await _context.Teams.ToListAsync());
            }
            foreach (var item in lst)
            {
                var X = await _context.Teams.FindAsync(item);
                if (X != null)
                {
                    Z.Add(X);
                }
            }
            if (Z == null)
            {
                return View(await _context.Teams.ToListAsync());
            }
            
            return View(Z);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }
            var Z = await _context.CompetitionTeams.FirstOrDefaultAsync(a => a.TeamId == id);
            return RedirectToAction("Details", "CompetitionTeams", new { id = Z.CompetitionTeamId }) ;
        }

        // GET: Teams/Create
        public IActionResult Create(int id,string name)
        {
            ViewBag.CompId0 = id;
            ViewBag.Name0 = name;
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,Name,FileDocument,Comment")] Team team,int CompId0)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","CompetitionTeams",new {id=CompId0,id1=team.TeamId });
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,Name,FileDocument,Comment")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
