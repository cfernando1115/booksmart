using BookSmart.Services;
using System;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookService BookService { get; }

        IShipmentService ShipmentService { get; }

        IGenreRepository Genres { get; }

        IMembershipTypeRepository MembershipTypes { get; }

        IMemberRepository Members { get;  }

        Task<int> CompleteAsync();
    }
}
