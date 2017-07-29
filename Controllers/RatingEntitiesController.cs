using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBook.Models;
using MovieBook.ViewModels;

namespace MovieBook.Controllers
{
    public class RatingEntitiesController : Controller
    {
        private readonly MovieBookDBContext _context;

        public RatingEntitiesController(MovieBookDBContext context)
        {
            _context = context;    
        }

        // GET: RatingEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.RatingEntities.ToListAsync());
        }

        // GET: RatingEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingEntities = await _context.RatingEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ratingEntities == null)
            {
                return NotFound();
            }

            return View(ratingEntities);
        }

        // GET: RatingEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RatingEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Desc")] RatingEntities ratingEntities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratingEntities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ratingEntities);
        }

        // GET: RatingEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingEntities = await _context.RatingEntities.SingleOrDefaultAsync(m => m.Id == id);
            if (ratingEntities == null)
            {
                return NotFound();
            }
            return View(ratingEntities);
        }

        // POST: RatingEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Desc")] RatingEntities ratingEntities)
        {
            if (id != ratingEntities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingEntities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingEntitiesExists(ratingEntities.Id))
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
            return View(ratingEntities);
        }

        // GET: RatingEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingEntities = await _context.RatingEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ratingEntities == null)
            {
                return NotFound();
            }

            return View(ratingEntities);
        }

        // POST: RatingEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingEntities = await _context.RatingEntities.SingleOrDefaultAsync(m => m.Id == id);
            _context.RatingEntities.Remove(ratingEntities);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RatingEntitiesExists(int id)
        {
            return _context.RatingEntities.Any(e => e.Id == id);
        }
    }
}
