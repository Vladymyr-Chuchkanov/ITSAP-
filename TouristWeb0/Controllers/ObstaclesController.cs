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
    public class ObstaclesController : Controller
    {
        private readonly DBLab1Context _context;

        public ObstaclesController(DBLab1Context context)
        {
            _context = context;
        }

        // GET: Obstacles
       /* public async Task<IActionResult> Index()
        {
            
            return View(await _context.Obstacles.ToListAsync());
        }*/
        public async Task<IActionResult> Index(int ?id)
        {
            ViewBag.CompId0 = null;
            if (id != null)
            {
                ViewBag.CompId0 = id;
            }
            return View(await _context.Obstacles.ToListAsync());
        }

        // GET: Obstacles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obstacle = await _context.Obstacles
                .FirstOrDefaultAsync(m => m.ObstacleId == id);
            if (obstacle == null)
            {
                return NotFound();
            }

            return View(obstacle);
        }

        // GET: Obstacles/Create
        /*public IActionResult Create()
        {
            return View();
        }*/
       /* public IActionResult Create()
        {
            
            return View();
        }*/
        public IActionResult Create(int? id)
        {
            ViewBag.CompId0 = null;
            if (id != null) { ViewBag.CompId0 = id; }

            return View();
        }

        // POST: Obstacles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObstacleId,Name,AdditionalDescription,EquipmentStart,EquipmentObstacle,EquipmentTarget,Length,Height,MovementFirst,ConditionsOvercoming,ConditionalComplexity,ObstacleEvaluation")] Obstacle obstacle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obstacle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obstacle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create0([Bind("ObstacleId,Name,AdditionalDescription,EquipmentStart,EquipmentObstacle,EquipmentTarget,Length,Height,MovementFirst,ConditionsOvercoming,ConditionalComplexity,ObstacleEvaluation")] Obstacle obstacle,string CompId0)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obstacle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","ObstacleCompetitions",new {idComp=CompId0,idOb=obstacle.ObstacleId });
            }
            ViewBag.CompId0 = CompId0;
            return View(obstacle);
        }
        public IActionResult Add(int ?id,int ?id1)
        {
            return RedirectToAction("Create", "ObstacleCompetitions", new { idComp = id, idOb = id1 });
        }
        // GET: Obstacles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obstacle = await _context.Obstacles.FindAsync(id);
            if (obstacle == null)
            {
                return NotFound();
            }
            return View(obstacle);
        }

        // POST: Obstacles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObstacleId,Name,AdditionalDescription,EquipmentStart,EquipmentObstacle,EquipmentTarget,Length,Height,MovementFirst,ConditionsOvercoming,ConditionalComplexity,ObstacleEvaluation")] Obstacle obstacle)
        {
            if (id != obstacle.ObstacleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(obstacle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObstacleExists(obstacle.ObstacleId))
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
            return View(obstacle);
        }

        // GET: Obstacles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obstacle = await _context.Obstacles
                .FirstOrDefaultAsync(m => m.ObstacleId == id);
            if (obstacle == null)
            {
                return NotFound();
            }

            return View(obstacle);
        }

        // POST: Obstacles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obstacle = await _context.Obstacles.FindAsync(id);
            _context.Obstacles.Remove(obstacle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObstacleExists(int id)
        {
            return _context.Obstacles.Any(e => e.ObstacleId == id);
        }
    }
}
