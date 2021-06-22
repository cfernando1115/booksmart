using System;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }

        IShipmentRepository Shipments { get; }


        IMemberRepository Members { get; }

        IGenreRepository Genres { get; }

        IMembershipTypeRepository MembershipTypes { get; }

        bool HasChanges();

        Task<int> CompleteAsync();
    }
}
