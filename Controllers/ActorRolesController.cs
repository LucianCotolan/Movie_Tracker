using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Tracker.Data;
using Movie_Tracker.Models;

namespace Movie_Tracker.Controllers
{
    [Authorize(Policy = "Manager")]
    public class ActorRolesController : Controller
    {
        private readonly MovieContext _context;

        public ActorRolesController(MovieContext context)
        {
            _context = context;
        }

        // GET: ActorRoles
        public async Task<IActionResult> Index()
        {
            var movieContext = _context.ActorRoles.Include(a => a.Actor).Include(a => a.Movie);
            return View(await movieContext.ToListAsync());
        }

        // GET: ActorRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRole = await _context.ActorRoles
                .Include(a => a.Actor)
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorRole == null)
            {
                return NotFound();
            }

            return View(actorRole);
        }

        // GET: ActorRoles/Create
        public IActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Name");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title");
            return View();
        }

        // POST: ActorRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,ActorId,RoleName")] ActorRole actorRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Id", actorRole.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", actorRole.MovieId);
            return View(actorRole);
        }

        // GET: ActorRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRole = await _context.ActorRoles.FindAsync(id);
            if (actorRole == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Id", actorRole.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", actorRole.MovieId);
            return View(actorRole);
        }

        // POST: ActorRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,ActorId,RoleName")] ActorRole actorRole)
        {
            if (id != actorRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorRoleExists(actorRole.Id))
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
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "Id", actorRole.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", actorRole.MovieId);
            return View(actorRole);
        }

        // GET: ActorRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRole = await _context.ActorRoles
                .Include(a => a.Actor)
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorRole == null)
            {
                return NotFound();
            }

            return View(actorRole);
        }

        // POST: ActorRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorRole = await _context.ActorRoles.FindAsync(id);
            _context.ActorRoles.Remove(actorRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorRoleExists(int id)
        {
            return _context.ActorRoles.Any(e => e.Id == id);
        }
    }
}
