using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Tracker.Data;
using Movie_Tracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Movie_Tracker.Controllers
{
    public class WatchedMoviesController : Controller
    {
        private readonly MovieContext _context;

        public WatchedMoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: WatchedMovies
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            
            var movieContext = _context.WatchedMovies.Where(w => w.ApplicationUserId == userId).Include(w => w.Movie);
            return View(await movieContext.ToListAsync());
        }

        // GET: WatchedMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchedMovie = await _context.WatchedMovies
                .Include(w => w.ApplicationUser)
                .Include(w => w.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchedMovie == null)
            {
                return NotFound();
            }

            return View(watchedMovie);
        }

        // GET: WatchedMovies/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: WatchedMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,ApplicationUserId")] WatchedMovie watchedMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watchedMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", watchedMovie.ApplicationUserId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", watchedMovie.MovieId);
            return View(watchedMovie);
        }

        // GET: WatchedMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchedMovie = await _context.WatchedMovies.FindAsync(id);
            if (watchedMovie == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", watchedMovie.ApplicationUserId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", watchedMovie.MovieId);
            return View(watchedMovie);
        }

        // POST: WatchedMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,ApplicationUserId")] WatchedMovie watchedMovie)
        {
            if (id != watchedMovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchedMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchedMovieExists(watchedMovie.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", watchedMovie.ApplicationUserId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", watchedMovie.MovieId);
            return View(watchedMovie);
        }

        // GET: WatchedMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watchedMovie = await _context.WatchedMovies
                .Include(w => w.ApplicationUser)
                .Include(w => w.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (watchedMovie == null)
            {
                return NotFound();
            }

            return View(watchedMovie);
        }

        // POST: WatchedMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var watchedMovie = await _context.WatchedMovies.FindAsync(id);
            _context.WatchedMovies.Remove(watchedMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchedMovieExists(int id)
        {
            return _context.WatchedMovies.Any(e => e.Id == id);
        }
    }
}
