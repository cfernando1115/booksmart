using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookSmart.Models;
using System.Linq;
using BookSmart.Utility;

namespace BookSmart.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<PagedList<Book>> GetBooksWithGenresAsync(BookParams bookParams);

        Task<Book> GetBookWithGenreAsync(int? id);

        IQueryable<Book> GetBooks();

    }
}
