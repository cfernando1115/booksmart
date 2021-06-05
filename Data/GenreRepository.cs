using BookSmart.Interfaces;
using BookSmart.Models;
using Repository;

namespace BookSmart.Data
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
            : base(context) 
        {
            _context = context;
        }
    }
}
