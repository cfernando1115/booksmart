using BookSmart.Interfaces;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IMemberService MemberService { get; private set; }

        public IBookService BookService { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IMembershipTypeRepository MembershipTypes { get; private set; }

        public IShipmentService ShipmentService { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IMemberService memberService, IBookService bookService, IShipmentService shipmentService, IGenreRepository genreRepository, IMembershipTypeRepository membershiptypeRepository)
        {
            _context = context;
            MemberService = memberService;
            ShipmentService = shipmentService;
            BookService = bookService;
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
