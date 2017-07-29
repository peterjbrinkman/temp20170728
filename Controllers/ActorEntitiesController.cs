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
    public class ActorEntitiesController : Controller
    {
        private readonly MovieBookDBContext _context;

        public ActorEntitiesController(MovieBookDBContext context)
        {
            _context = context;    
        }

        // GET: ActorEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActorEntities.ToListAsync());
        }

        // GET: ActorEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorEntities = await _context.ActorEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (actorEntities == null)
            {
                return NotFound();
            }

            return View(actorEntities);
        }

        // GET: ActorEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActorEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bill,Name")] ActorEntities actorEntities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorEntities);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(actorEntities);
        }

        // GET: ActorEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorEntities = await _context.ActorEntities.SingleOrDefaultAsync(m => m.Id == id);
            if (actorEntities == null)
            {
                return NotFound();
            }
            return View(actorEntities);
        }

        // POST: ActorEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bill,Name")] ActorEntities actorEntities)
        {
            if (id != actorEntities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorEntities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorEntitiesExists(actorEntities.Id))
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
            return View(actorEntities);
        }

        // GET: ActorEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorEntities = await _context.ActorEntities
                .SingleOrDefaultAsync(m => m.Id == id);
            if (actorEntities == null)
            {
                return NotFound();
            }

            return View(actorEntities);
        }

        // POST: ActorEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorEntities = await _context.ActorEntities.SingleOrDefaultAsync(m => m.Id == id);
            _context.ActorEntities.Remove(actorEntities);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ActorEntitiesExists(int id)
        {
            return _context.ActorEntities.Any(e => e.Id == id);
        }
    }
}
