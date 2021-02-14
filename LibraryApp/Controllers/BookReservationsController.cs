using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryApp.Controllers
{
    [Authorize]
    public class BookReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookReservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookReservations.Include(b => b.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservations
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.BookReservationId == id);
            if (bookReservation == null)
            {
                return NotFound();
            }

            return View(bookReservation);
        }

        // GET: BookReservations/Create
        public IActionResult Create(int? id)
        {
            ViewData["SpecyficBookId"] = id;
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            //var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            return View();
        }

        // POST: BookReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookReservationId,ReservationDate,BookId,UserId,UserName")] BookReservation bookReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookReservation.BookId);
            ViewData["SpecyficBookReservation"] = bookReservation.BookId;
            return View(bookReservation);
        }

        // GET: BookReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservations.FindAsync(id);
            if (bookReservation == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookReservation.BookId);
            return View(bookReservation);
        }

        // POST: BookReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookReservationId,ReservationDate,BookId,UserId,UserName")] BookReservation bookReservation)
        {
            if (id != bookReservation.BookReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookReservationExists(bookReservation.BookReservationId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookReservation.BookId);
            return View(bookReservation);
        }

        // GET: BookReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReservation = await _context.BookReservations
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.BookReservationId == id);
            if (bookReservation == null)
            {
                return NotFound();
            }

            return View(bookReservation);
        }

        // POST: BookReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookReservation = await _context.BookReservations.FindAsync(id);
            _context.BookReservations.Remove(bookReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookReservationExists(int id)
        {
            return _context.BookReservations.Any(e => e.BookReservationId == id);
        }
    }
}
