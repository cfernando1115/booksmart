using BookSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAfterDate(DateTime dateAdded);

        Task<IEnumerable<Book>> GetBooksBeforeDate(DateTime dateAdded);
    }
}
