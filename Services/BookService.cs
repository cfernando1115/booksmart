using BookSmart.Data;
using BookSmart.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public class BookService : Repository<Book>, IBookService
    {
        public BookService(ApplicationDbContext context)
    :        base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<IEnumerable<Book>> GetBooksAfterDate(DateTime date)
        {
            return await ApplicationDbContext.Books.Where(b => b.DateAdded > date).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksBeforeDate(DateTime date)
        {
            return await ApplicationDbContext.Books.Where(b => b.DateAdded < date).ToListAsync();
        }
    }
}
