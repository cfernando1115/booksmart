using BookSmart.Interfaces;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBookRepository Books { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IMembershipTypeRepository MembershipTypes { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IBookRepository bookRepository, IGenreRepository genreRepository, IMembershipTypeRepository membershiptypeRepository)
        {
            _context = context;
            Books = bookRepository;
            Genres = genreRepository;
            MembershipTypes = membershiptypeRepository;
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
