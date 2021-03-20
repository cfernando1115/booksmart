using System;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IGenreRepository Genres { get; }

        Task<int> CompleteAsync();
    }
}
