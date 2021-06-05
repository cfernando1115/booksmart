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

        public IMemberService MemberService => new MemberService(_context);

        public IBookService BookService => new BookService(_context);

        public IGenreRepository Genres => new GenreRepository(_context);

        public IMembershipTypeRepository MembershipTypes => new MembershipTypeRepository(_context);

        public IShipmentService ShipmentService => new ShipmentService(_context);

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
