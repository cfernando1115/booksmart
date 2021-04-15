using BookSmart.Interfaces;
using BookSmart.Services;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBookService BookService { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IMembershipTypeRepository MembershipTypes { get; private set; }

        public IMemberRepository Members { get; private set; }

        public IShipmentService ShipmentService { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IBookService bookService, IShipmentService shipmentService, IGenreRepository genreRepository, IMembershipTypeRepository membershiptypeRepository, IMemberRepository memberRepository)
        {
            _context = context;
            ShipmentService = shipmentService;
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
