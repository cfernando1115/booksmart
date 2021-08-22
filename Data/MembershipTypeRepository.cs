using BookSmart.Interfaces;
using BookSmart.Models;
using Repository;

namespace BookSmart.Data
{
    public class MembershipTypeRepository : Repository<MembershipType>, IMembershipTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public MembershipTypeRepository(ApplicationDbContext context)
              : base(context)
        {
            _context = context;
        }
    }
}
