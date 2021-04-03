using BookSmart.Services;
using System;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }

        IBookService BookService { get; }

        IGenreRepository Genres { get; }

        IMembershipTypeRepository MembershipTypes { get; }

        IMemberRepository Members { get;  }

        Task<int> CompleteAsync();
    }
}
