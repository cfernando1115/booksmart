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
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        public async Task<ActionResult<BookFormViewModel>> Create()
        {
            var genres = await _context.Genres.ToListAsync();
            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };
            return View(viewModel);
        }
    }
}
