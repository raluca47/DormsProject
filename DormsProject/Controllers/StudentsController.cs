using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DormsProject.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DormsProject.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DormsContext _context;

        public StudentsController(DormsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_name" : "";
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var query = _context.Students.Include(s => s.CnpNavigation).Include(s => s.RoomNumberNavigation)
			  .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.CnpNavigation.LastName.Contains(searchString) || s.CnpNavigation.FirstName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "last_name":
                    query = query.OrderBy(p => p.CnpNavigation.LastName);
                    break;

                case "first_name":
                    query = query.OrderBy(p => p.CnpNavigation.FirstName);
                    break;

                default:
                    query = query.OrderBy(p => p.CnpNavigation.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(await query.ToPagedListAsync(pageNumber, pageSize));
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CnpNavigation).Include(s=>s.RoomNumberNavigation)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Cnp"] = new SelectList(_context.People, "Cnp", "Cnp");
            var availableRooms = _context.Rooms
               .Where(room => room.Isoccupied == false)
               .Select(room => new { room.RoomNumber })
               .ToList();

            ViewData["RoomNumber"] = new SelectList(availableRooms, "RoomNumber", "RoomNumber");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudyYear,FormOfEducation,Cnp,RoomNumber, RoomNumberNavigation")] Student student)
        {
            var selectedRoom = await _context.Rooms.FirstOrDefaultAsync(room => room.RoomNumber == student.RoomNumber);

            if (selectedRoom != null)
            {
                selectedRoom.Isoccupied = true; // Set the room as occupied
                _context.Update(selectedRoom);   // Update the room entity in the context
            }
            _context.Add(student);
            await _context.SaveChangesAsync();

            ViewData["Cnp"] = new SelectList(_context.People, "Cnp", "Cnp", student.Cnp);
            var availableRooms = _context.Rooms
               .Where(room => room.Isoccupied == false)
               .Select(room => new { room.RoomNumber })
               .ToList();

            ViewData["RoomNumber"] = new SelectList(availableRooms, "RoomNumber", "RoomNumber", student.RoomNumber);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Cnp"] = new SelectList(_context.People, "Cnp", "Cnp", student.Cnp);
            var availableRooms = _context.Rooms
               .Where(room => room.Isoccupied == false)
               .Select(room => new { room.RoomNumber })
               .ToList();

            ViewData["RoomNumber"] = new SelectList(availableRooms, "RoomNumber", "RoomNumber", student.RoomNumber);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudyYear,FormOfEducation,Cnp,RoomNumber")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }
            try
                {
                var selectedRoom = await _context.Rooms.FirstOrDefaultAsync(room => room.RoomNumber == student.RoomNumber);

                if (selectedRoom != null)
                {
                    selectedRoom.Isoccupied = true; // Set the room as occupied
                    _context.Update(selectedRoom);   // Update the room entity in the context
                }
                _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
           
            ViewData["Cnp"] = new SelectList(_context.People, "Cnp", "Cnp", student.Cnp);
            var availableRooms = _context.Rooms
               .Where(room => room.Isoccupied == false)
               .Select(room => new { room.RoomNumber })
               .ToList();

            ViewData["RoomNumber"] = new SelectList(availableRooms, "RoomNumber", "RoomNumber", student.RoomNumber);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CnpNavigation)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'DormsContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
