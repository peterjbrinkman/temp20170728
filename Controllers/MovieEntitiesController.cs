using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieBook.Models;
using MovieBook.ViewModels;

namespace MovieBook.Controllers
{
    public class MovieEntitiesController : Controller
    {
        private readonly MovieBookDBContext _context;

        public MovieEntitiesController(MovieBookDBContext context)
        {
            _context = context;
        }

        // GET: MovieEntities
        public async Task<IActionResult> Index()
        {
            var movieEntities = await _context.MovieEntities
                .Join(_context.RatingEntities,
                    m => m.RatingId,
                    r => r.Id,
                    (m, r) => new MovieEntitiesIndexViewModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Desc = m.Desc,
                        Rating = r.Name
                    })
                    .ToListAsync();

            if (movieEntities == null)
            {
                return NotFound();
            }

            return View(movieEntities);
        }

        private async Task<String> GetRating(int id)
        {
            var rating = await _context.RatingEntities
                .SingleOrDefaultAsync(r => r.Id == id);

            return rating.Name;
        }

        private IEnumerable<SelectListItem> GetRatings()
        {
            var rating = _context.RatingEntities
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name
                                });

            return new SelectList(rating, "Value", "Text");
        }

        // GET: MovieEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieEntities = await _context.MovieEntities
                .Join(_context.RatingEntities,
                    m => m.RatingId,
                    r => r.Id,
                    (m, r) => new MovieEntitiesIndexViewModel
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Desc = m.Desc,
                        Rating = r.Name
                    })
                    .Where(o => o.Id == id)
                    .ToListAsync();

            if (movieEntities == null)
            {
                return NotFound();
            }

            return View(movieEntities);
        }

        // GET: MovieEntities/Create
        public IActionResult Create()
        {
            var model = new MovieEntitiesViewModel
            {
                Rating = GetRatings()
            };
            return View(model);
        }

        // POST: MovieEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Desc,RatingId")] MovieEntitiesViewModel movieEntities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieEntities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movieEntities);
        }

        // GET: MovieEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieEntities = await _context.MovieEntities.SingleOrDefaultAsync(m => m.Id == id);
            if (movieEntities == null)
            {
                return NotFound();
            }

            var model = new MovieEntitiesViewModel
            {
                Id = movieEntities.Id,
                Title = movieEntities.Title,
                Desc = movieEntities.Desc,
                SelectedRatingId = movieEntities.RatingId,
                Rating = GetRatings()
            };
            return View(model);

        }

        // POST: MovieEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Desc,SelectedRatingId")] MovieEntitiesViewModel movieEntities)
        {
            if (id != movieEntities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var editModel = new MovieEntities
                {
                    Id = movieEntities.Id.Value,
                    Title = movieEntities.Title,
                    Desc = movieEntities.Desc,
                    RatingId = movieEntities.SelectedRatingId
                };
                try
                {
                    _context.Update(editModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieEntitiesExists(editModel.Id))
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
            return View(movieEntities);
        }

        // GET: MovieEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieEntities = await _context.MovieEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (movieEntities == null)
            {
                return NotFound();
            }

            return View(movieEntities);
        }

        // POST: MovieEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieEntities = await _context.MovieEntities.SingleOrDefaultAsync(m => m.Id == id);
            _context.MovieEntities.Remove(movieEntities);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MovieEntitiesExists(int id)
        {
            return _context.MovieEntities.Any(e => e.Id == id);
        }
    }
}
