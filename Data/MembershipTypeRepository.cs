using BookSmart.Interfaces;
using BookSmart.Models;
using Repository;

namespace BookSmart.Data
{
    public class MembershipTypeRepository : Repository<MembershipType>, IMembershipTypeRepository
    {
      public MembershipTypeRepository(ApplicationDbContext context)
        : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
