using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.Data;
using Microsoft.EntityFrameworkCore;
using BookSmart.ViewModels;

namespace BookSmart.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Book>>> Index()
        {
            var books = await _context.Books.Include(b=>b.Genre).ToListAsync();

            return View(books);
        }

        [HttpGet("Create")]
        public async Task<ActionResult<BookFormViewModel>> Create()
        {
            var genres = await _context.Genres.ToListAsync();

            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View(viewModel);
        }
        
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Name = model.Book.Name,
                    Author = model.Book.Author,
                    Price = model.Book.Price,
                    GenreId = model.Book.GenreId
                };

                _context.Books.Add(book);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id?}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(b => b.Genre).FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost("Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
