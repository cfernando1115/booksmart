using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.ViewModels;
using BookSmart.Interfaces;
using BookSmart.Extensions;
using System;

namespace BookSmart.Controllers
{
    [Route("Book")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<IEnumerable<Book>>> Index()
        {
            var books = await _unitOfWork.BookService.GetBooksWithGenresAsync();
            return View(books);
        }

        [HttpGet("Featured")]
        public async Task<ActionResult<IEnumerable<Book>>> Featured()
        {
            var member = await _unitOfWork.MemberService.GetMemberByUsernameAsync(User.GetUsername());

            var lastLogin = member.LastLogin ?? DateTime.Today;

            var numBooks = Utility.FeatureHelper.NumBooks;

            var newBooks = await _unitOfWork.BookService.GetBooksAfterDate(lastLogin, numBooks) ?? await _unitOfWork.BookService.GetBooksBeforeDate(lastLogin, numBooks);

            var memberBooksModel = new MemberBooksViewModel
            {
                Member = member,
                Books = newBooks?.ToList()
            };

            return View(memberBooksModel);
        }

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

                _unitOfWork.BookService.Add(book);

                await _unitOfWork.CompleteAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id?}")]
        public async Task<ActionResult<Book>> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var book = await _unitOfWork.BookService.GetBookWithGenreAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost("Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook(int? id)
        {
            var book = _unitOfWork.BookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _unitOfWork.BookService.Remove(book);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        [HttpGet("Update/{id?}")]
        public async Task<ActionResult<BookFormViewModel>> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var book = await _unitOfWork.BookService.GetBookWithGenreAsync(id);

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

        [HttpPost("Update/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(BookFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Book updatedBook = viewModel.Book;

                var book = _unitOfWork.BookService.Get(updatedBook.Id);

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

            var book = await _unitOfWork.BookService.GetBookWithGenreAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
