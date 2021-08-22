using BookSmart.Models;
using BookSmart.Utility;
using Repository;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<PagedList<Book>> GetBooksWithGenresAsync(BookParams bookParams);

        Task<Book> GetBookWithGenreAsync(int? id);

        IQueryable<Book> GetBooks();

    }
}
