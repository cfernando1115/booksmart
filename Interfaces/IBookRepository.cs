using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookSmart.Models;

namespace BookSmart.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksWithGenresAsync();

        Task<Book> GetBookWithGenreAsync(int? id);
    }
}
