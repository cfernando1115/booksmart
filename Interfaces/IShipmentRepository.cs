using BookSmart.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IShipmentRepository : IRepository<Shipment>
    {
        IQueryable<Shipment> GetShipments();

        IQueryable<Shipment> GetShipment(int id);

        Task<Shipment> GetShipmentAsync(int id);
    }
}
