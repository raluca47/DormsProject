using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormsProject.Models;
using X.PagedList;

namespace DormsProject.Controllers
{
    public class RoomsController : Controller
    {
        private readonly DormsContext _context;

        public RoomsController(DormsContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var dormsContext = _context.Rooms.AsQueryable(); // Ensure dormsContext is IQueryable

            if (searchString != null)
            {
                dormsContext = dormsContext.Where(s => s.Floor.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "floor":
                    dormsContext = dormsContext.OrderBy(p => p.Floor);
                    break;


                default:
                    dormsContext = dormsContext.OrderBy(p => p.Floor);
                    break;
            }

            dormsContext = dormsContext.Include(r => r.Dorm); // Include Address after sorting

            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(await dormsContext.ToPagedListAsync(pageNumber, pageSize));
        }


        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Dorm)
                .FirstOrDefaultAsync(m => m.RoomNumber == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["DormId"] = new SelectList(_context.Dorms, "DormId", "DormId");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNumber,Floor,DormId, Isoccupied")] Room room)
        {

                _context.Add(room);
                await _context.SaveChangesAsync();
            ViewData["DormId"] = new SelectList(_context.Dorms, "DormId", "DormId", room.DormId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["DormId"] = new SelectList(_context.Dorms, "DormId", "DormId", room.DormId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomNumber,Floor,DormId,Isoccupied")] Room room)
        {
            if (id != room.RoomNumber)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            ViewData["DormId"] = new SelectList(_context.Dorms, "DormId", "DormId", room.DormId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Dorm)
                .FirstOrDefaultAsync(m => m.RoomNumber == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'DormsContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
          return (_context.Rooms?.Any(e => e.RoomNumber == id)).GetValueOrDefault();
        }
    }
}
