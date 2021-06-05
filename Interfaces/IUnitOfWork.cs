using System;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookService BookService { get; }

        IShipmentService ShipmentService { get; }

        IMemberService MemberService { get; }

        IGenreRepository Genres { get; }

        IMembershipTypeRepository MembershipTypes { get; }

        bool HasChanges();

        Task<int> CompleteAsync();
    }
}
