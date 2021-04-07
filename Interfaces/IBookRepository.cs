using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookSmart.Models;
using System.Linq;

namespace BookSmart.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithGenresAsync();

        Task<Book> GetBookWithGenreAsync(int? id);

        IQueryable<Book> GetBooks();

    }
}
