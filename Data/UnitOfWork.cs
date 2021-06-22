using BookSmart.Interfaces;
using BookSmart.Services;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IMemberRepository Members => new MemberRepository(_context);

        public IBookRepository Books => new BookRepository(_context);

        public IShipmentRepository Shipments => new ShipmentRepository(_context);

        public IGenreRepository Genres => new GenreRepository(_context);

        public IMembershipTypeRepository MembershipTypes => new MembershipTypeRepository(_context);

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
