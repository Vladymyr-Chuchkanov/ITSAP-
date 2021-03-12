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
    public class TeamPartisipantsController : Controller
    {
        private readonly DBLab1Context _context;

        public TeamPartisipantsController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: TeamPartisipants
        public async Task<IActionResult> Index()
        {
            var dBLab1Context = _context.TeamPartisipants.Include(t => t.Partisipant).Include(t => t.Team);
            return View(await dBLab1Context.ToListAsync());
        }

        // GET: TeamPartisipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPartisipant = await _context.TeamPartisipants
                .Include(t => t.Partisipant)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamPartisipantId == id);
            if (teamPartisipant == null)
            {
                return NotFound();
            }

            return View(teamPartisipant);
        }

        // GET: TeamPartisipants/Create
        public IActionResult Create()
        {
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Comment");
            return View();
        }

        // POST: TeamPartisipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamPartisipantId,PartisipantId,TeamId,Participated")] TeamPartisipant teamPartisipant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamPartisipant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", teamPartisipant.PartisipantId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Comment", teamPartisipant.TeamId);
            return View(teamPartisipant);
        }

        // GET: TeamPartisipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPartisipant = await _context.TeamPartisipants.FindAsync(id);
            if (teamPartisipant == null)
            {
                return NotFound();
            }
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", teamPartisipant.PartisipantId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Comment", teamPartisipant.TeamId);
            return View(teamPartisipant);
        }

        // POST: TeamPartisipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamPartisipantId,PartisipantId,TeamId,Participated")] TeamPartisipant teamPartisipant)
        {
            if (id != teamPartisipant.TeamPartisipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamPartisipant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamPartisipantExists(teamPartisipant.TeamPartisipantId))
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
            ViewData["PartisipantId"] = new SelectList(_context.Partisipants, "ParticipantId", "Name", teamPartisipant.PartisipantId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "Comment", teamPartisipant.TeamId);
            return View(teamPartisipant);
        }

        // GET: TeamPartisipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamPartisipant = await _context.TeamPartisipants
                .Include(t => t.Partisipant)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamPartisipantId == id);
            if (teamPartisipant == null)
            {
                return NotFound();
            }

            return View(teamPartisipant);
        }

        // POST: TeamPartisipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamPartisipant = await _context.TeamPartisipants.FindAsync(id);
            _context.TeamPartisipants.Remove(teamPartisipant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamPartisipantExists(int id)
        {
            return _context.TeamPartisipants.Any(e => e.TeamPartisipantId == id);
        }
    }
}
