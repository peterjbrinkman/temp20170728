using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieBook.Models;

namespace MovieBook.Controllers
{
    public class GenreEntitiesController : Controller
    {
        private readonly MovieBookDBContext _context;

        public GenreEntitiesController(MovieBookDBContext context)
        {
            _context = context;    
        }

        // GET: GenreEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.GenreEntities.ToListAsync());
        }

        // GET: GenreEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreEntities = await _context.GenreEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (genreEntities == null)
            {
                return NotFound();
            }

            return View(genreEntities);
        }

        // GET: GenreEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenreEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Desc")] GenreEntities genreEntities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genreEntities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(genreEntities);
        }

        // GET: GenreEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreEntities = await _context.GenreEntities.SingleOrDefaultAsync(m => m.Id == id);
            if (genreEntities == null)
            {
                return NotFound();
            }
            return View(genreEntities);
        }

        // POST: GenreEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Desc")] GenreEntities genreEntities)
        {
            if (id != genreEntities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genreEntities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreEntitiesExists(genreEntities.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(genreEntities);
        }

        // GET: GenreEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreEntities = await _context.GenreEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (genreEntities == null)
            {
                return NotFound();
            }

            return View(genreEntities);
        }

        // POST: GenreEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreEntities = await _context.GenreEntities.SingleOrDefaultAsync(m => m.Id == id);
            _context.GenreEntities.Remove(genreEntities);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GenreEntitiesExists(int id)
        {
            return _context.GenreEntities.Any(e => e.Id == id);
        }
    }
}
