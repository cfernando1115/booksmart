using BookSmart.Interfaces;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBookRepository Books { get; private set; }
        public IGenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _context = context;
            Books = bookRepository;
            Genres = genreRepository;
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
