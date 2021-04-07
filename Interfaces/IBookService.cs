using BookSmart.Interfaces;
using BookSmart.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public interface IBookService : IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAfterDate(DateTime dateAdded, int maxNumberOfBooks);

        Task<IEnumerable<Book>> GetBooksBeforeDate(DateTime dateAdded, int maxNumberOfBooks);
    }
}
