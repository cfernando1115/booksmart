using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetBooksAfterDate(DateTime date, int maxNumberOfBooks)
        {
            var books = _unitOfWork.Books.GetBooks();

            var numberOfBooks = await books
                .Where(b => b.DateAdded >= date)
                .CountAsync();

            if(numberOfBooks != 0)
            {
                var booksToTake = DetermineBooksToTake(numberOfBooks, maxNumberOfBooks);

                return await books
                    .Where(b => b.DateAdded >= date)
                    .Include(b=>b.Genre)
                    .Take(booksToTake)
                    .ToListAsync();
            }

            return null;
        }

        public async Task<IEnumerable<Book>> GetBooksBeforeDate(DateTime date, int maxNumberOfBooks)
        {
            var books = _unitOfWork.Books.GetBooks();

            var numberOfBooks = await books
                .Where(b => b.DateAdded < date)
                .CountAsync();

            var booksToTake = DetermineBooksToTake(numberOfBooks, maxNumberOfBooks);

            return await books
                .Where(b => b.DateAdded < date)
                .Include(b => b.Genre)
                .OrderByDescending(b => b.DateAdded)
                .Take(booksToTake)
                .ToListAsync();
        }

        private int DetermineBooksToTake(int totalBooks, int maxBooks)
        {
            return totalBooks > maxBooks
                ? maxBooks
                : totalBooks;
        }
    }
}
