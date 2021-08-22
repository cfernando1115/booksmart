using BookSmart.Interfaces;
using BookSmart.Models;
using Repository;
using System.Linq;
using System.Threading.Tasks;

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

        public IQueryable<Shipment> GetShipments()
        {
            return _context.Shipments.AsQueryable();
        }

        public IQueryable<Shipment> GetShipment(int id)
        {
            return _context.Shipments.Where(s => s.Id == id).AsQueryable();
        }

        public async Task<Shipment> GetShipmentAsync(int id)
        {
            return await _context.Shipments.FindAsync(id);
        }
    }
}
