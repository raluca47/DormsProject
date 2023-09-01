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
    public class DormsController : Controller
    {
        private readonly DormsContext _context;

        public DormsController(DormsContext context)
        {
            _context = context;
        }

		// GET: Dorms

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
            
			var dormsContext = _context.Dorms.AsQueryable(); // Ensure dormsContext is IQueryable
			if (searchString != null)
			{
				dormsContext = dormsContext.Where(s => s.AdressId.ToString().Contains(searchString)
                                       || s.ComplexId.ToString().Contains(searchString));
            }

			dormsContext = dormsContext.Include(d => d.Adress).Include(d => d.Complex);

			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(dormsContext.ToPagedList(pageNumber, pageSize));

		}

        // GET: Dorms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dorms == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms
                .Include(d => d.Adress)
                .Include(d => d.Complex)
                .FirstOrDefaultAsync(m => m.DormId == id);
            if (dorm == null)
            {
                return NotFound();
            }

            return View(dorm);
        }

        // GET: Dorms/Create
        public IActionResult Create()
        {
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            ViewData["ComplexId"] = new SelectList(_context.Complexes, "ComplexId", "ComplexId");
            return View();
        }

        // POST: Dorms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DormId,AdressId,ComplexId")] Dorm dorm)
        {
                _context.Add(dorm);
                await _context.SaveChangesAsync();
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", dorm.AdressId);
            ViewData["ComplexId"] = new SelectList(_context.Complexes, "ComplexId", "ComplexId", dorm.ComplexId);
            return View(dorm);
        }

        // GET: Dorms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dorms == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms.FindAsync(id);
            if (dorm == null)
            {
                return NotFound();
            }
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", dorm.AdressId);
            ViewData["ComplexId"] = new SelectList(_context.Complexes, "ComplexId", "ComplexId", dorm.ComplexId);
            return View(dorm);
        }

        // POST: Dorms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DormId,AdressId,ComplexId")] Dorm dorm)
        {
            if (id != dorm.DormId)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(dorm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormExists(dorm.DormId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            
            ViewData["AdressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", dorm.AdressId);
            ViewData["ComplexId"] = new SelectList(_context.Complexes, "ComplexId", "ComplexId", dorm.ComplexId);
            return View(dorm);
        }

        // GET: Dorms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dorms == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms
                .Include(d => d.Adress)
                .Include(d => d.Complex)
                .FirstOrDefaultAsync(m => m.DormId == id);
            if (dorm == null)
            {
                return NotFound();
            }

            return View(dorm);
        }

        // POST: Dorms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dorms == null)
            {
                return Problem("Entity set 'DormsContext.Dorms'  is null.");
            }
            var dorm = await _context.Dorms.FindAsync(id);
            if (dorm != null)
            {
                _context.Dorms.Remove(dorm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormExists(int id)
        {
          return (_context.Dorms?.Any(e => e.DormId == id)).GetValueOrDefault();
        }
    }
}
