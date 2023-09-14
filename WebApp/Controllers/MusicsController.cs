using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMusics.Data.MvcMusic;

namespace WebApp.Controllers
{
    public class MusicsController : Controller
    {
        private readonly Context _context;

        public MusicsController(Context context)
        {
            _context = context;
        }

        // GET: Musics
        public async Task<IActionResult> Index()
        {
              return _context.Musics != null ? 
                          View(await _context.Musics.ToListAsync()) :
                          Problem("Entity set 'Context.Musics'  is null.");
        }

        // GET: Musics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Musics == null)
            {
                return NotFound();
            }

            var musics = await _context.Musics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musics == null)
            {
                return NotFound();
            }

            return View(musics);
        }

        // GET: Musics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre")] Musics musics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musics);
        }

        // GET: Musics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Musics == null)
            {
                return NotFound();
            }

            var musics = await _context.Musics.FindAsync(id);
            if (musics == null)
            {
                return NotFound();
            }
            return View(musics);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre")] Musics musics)
        {
            if (id != musics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicsExists(musics.Id))
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
            return View(musics);
        }

        // GET: Musics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Musics == null)
            {
                return NotFound();
            }

            var musics = await _context.Musics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musics == null)
            {
                return NotFound();
            }

            return View(musics);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Musics == null)
            {
                return Problem("Entity set 'Context.Musics'  is null.");
            }
            var musics = await _context.Musics.FindAsync(id);
            if (musics != null)
            {
                _context.Musics.Remove(musics);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicsExists(int id)
        {
          return (_context.Musics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
