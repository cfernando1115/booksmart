using BookSmart.Interfaces;
using BookSmart.Models;
using Repository;

namespace BookSmart.Data
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(ApplicationDbContext context)
            : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}
