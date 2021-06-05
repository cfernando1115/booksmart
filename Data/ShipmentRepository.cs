using BookSmart.Interfaces;
using BookSmart.Models;
using Repository;

namespace BookSmart.Data
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        private readonly ApplicationDbContext _context;
        public ShipmentRepository(ApplicationDbContext context)
            : base(context) 
        {
            _context = context;
        }
    }
}
