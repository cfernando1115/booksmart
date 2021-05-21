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

        public BookRepository(ApplicationDbContext context)
            : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<PagedList<Book>> GetBooksWithGenresAsync(BookParams bookParams) 
        {
            IQueryable<Book> query = ApplicationDbContext.Books
                .Include(b => b.Genre);
            
            if(bookParams.GenreId != 0)
            {
                query = query.Where(b => b.GenreId == bookParams.GenreId);
            }

            return await PagedList<Book>.CreateAsync(query, bookParams.PageNumber, bookParams.PageSize);
        }

        public async Task<Book> GetBookWithGenreAsync(int? id)
        {
            return await ApplicationDbContext.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public IQueryable<Book> GetBooks()
        {
            return ApplicationDbContext.Books.AsQueryable();
        }
    }
}
