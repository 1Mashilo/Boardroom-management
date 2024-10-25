using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using boardroom_management.Data;

namespace boardroom_management.Controllers
{
    public class BoardroomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boardrooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Boardrooms.ToListAsync());
        }

        // GET: Boardrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardroom = await _context.Boardrooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardroom == null)
            {
                return NotFound();
            }

            return View(boardroom);
        }

        // GET: Boardrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boardrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,Equipment,Location")] Boardroom boardroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boardroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boardroom);
        }

        // GET: Boardrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardroom = await _context.Boardrooms.FindAsync(id);
            if (boardroom == null)
            {
                return NotFound();
            }
            return View(boardroom);
        }

        // POST: Boardrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,Equipment,Location")] Boardroom boardroom)
        {
            if (id != boardroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boardroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardroomExists(boardroom.Id))
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
            return View(boardroom);
        }

        // GET: Boardrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boardroom = await _context.Boardrooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boardroom == null)
            {
                return NotFound();
            }

            return View(boardroom);
        }

        // POST: Boardrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardroom = await _context.Boardrooms.FindAsync(id);
            if (boardroom != null)
            {
                _context.Boardrooms.Remove(boardroom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardroomExists(int id)
        {
            return _context.Boardrooms.Any(e => e.Id == id);
        }
    }
}
