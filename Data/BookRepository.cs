using System.Collections.Generic;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq;
using BookSmart.Utility;

namespace BookSmart.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
            : base(context) 
        {
            _context = context;
        }


        public async Task<PagedList<Book>> GetBooksWithGenresAsync(BookParams bookParams) 
        {
            IQueryable<Book> query = _context.Books
                .Include(b => b.Genre);
            
            if(bookParams.GenreId != 0)
            {
                query = query.Where(b => b.GenreId == bookParams.GenreId);
            }

            return await PagedList<Book>.CreateAsync(query, bookParams.PageNumber, bookParams.PageSize);
        }

        public async Task<Book> GetBookWithGenreAsync(int? id)
        {
            return await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public IQueryable<Book> GetBooks()
        {
            return _context.Books.AsQueryable();
        }
    }
}
