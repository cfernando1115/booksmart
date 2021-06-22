using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.ViewModels;
using BookSmart.Interfaces;
using BookSmart.Extensions;
using System;
using Microsoft.AspNetCore.Authorization;
using BookSmart.Utility;
using Microsoft.Extensions.Configuration;

namespace BookSmart.Controllers
{
    [Authorize]
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public BookController(IUnitOfWork unitOfWork, IConfiguration config, IBookService bookService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _bookService = bookService;
        }

        public async Task<ActionResult<BookFilterViewModel>> Index([FromQuery]int? pageNumber, int? genreId, int? pageSize)
        {
            var bookParams = new BookParams
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? Convert.ToInt32(_config.GetValue<string>("BookPagination:PageSize")),
                GenreId = genreId ?? 0
            };

            var bookFilterViewModel = new BookFilterViewModel
            {
                Books = await _unitOfWork.Books.GetBooksWithGenresAsync(bookParams),
                Genres = _unitOfWork.Genres.GetAll()
            };
            
        return View(bookFilterViewModel);
        }

        [HttpGet("Featured")]
        public async Task<ActionResult<IEnumerable<Book>>> Featured()
        {
            var member = await _unitOfWork.Members.GetMemberByUsernameAsync(User.GetUsername());

            var lastLogin = member.LastLogin ?? DateTime.Today;

            var numBooks = Utility.FeatureHelper.NumBooks;

            var newBooks = await _bookService.GetBooksAfterDate(lastLogin, numBooks) ?? await _bookService.GetBooksBeforeDate(lastLogin, numBooks);

            var memberBooksModel = new MemberBooksViewModel
            {
                Member = member,
                Books = newBooks?.ToList()
            };

            return View(memberBooksModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Create")]
        public ActionResult<BookFormViewModel> Create()
        {
            var genres = _unitOfWork.Genres.GetAll().ToList();

            var viewModel = new BookFormViewModel
            {
                Genres = genres
            };
            return View(viewModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
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
                    GenreId = model.Book.GenreId,
                    Description = model.Book.Description
                };

                _unitOfWork.Books.Add(book);

                await _unitOfWork.CompleteAsync();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Delete/{id?}")]
        public async Task<ActionResult<Book>> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var book = await _unitOfWork.Books.GetBookWithGenreAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook(int? id)
        {
            var book = _unitOfWork.Books.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _unitOfWork.Books.Remove(book);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("Update/{id?}")]
        public async Task<ActionResult<BookFormViewModel>> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var book = await _unitOfWork.Books.GetBookWithGenreAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var genres = _unitOfWork.Genres.GetAll().ToList();

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = genres
            };

            return View(viewModel);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("Update/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(BookFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Book updatedBook = viewModel.Book;

                var book = _unitOfWork.Books.Get(updatedBook.Id);

                book.Name = updatedBook.Name;
                book.Author = updatedBook.Author;
                book.Price = updatedBook.Price;
                book.GenreId = updatedBook.GenreId;
                book.Description = updatedBook.Description;

                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpGet("Detail/{id?}")]
        public async Task<ActionResult<Book>> Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var book = await _unitOfWork.Books.GetBookWithGenreAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
