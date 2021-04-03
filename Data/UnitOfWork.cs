using BookSmart.Interfaces;
using BookSmart.Services;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBookRepository Books { get; private set; }

        public IBookService BookService { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IMembershipTypeRepository MembershipTypes { get; private set; }

        public IMemberRepository Members { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IBookRepository bookRepository, IBookService bookService, IGenreRepository genreRepository, IMembershipTypeRepository membershiptypeRepository, IMemberRepository memberRepository)
        {
            _context = context;
            Books = bookRepository;
            BookService = bookService;
            Genres = genreRepository;
            MembershipTypes = membershiptypeRepository;
            Members = memberRepository;
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
