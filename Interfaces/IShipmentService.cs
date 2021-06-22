using BookSmart.Models;
using BookSmart.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IShipmentService 
    {
        Task<int> AddUpdateAsync(ShipmentFormViewModel shipmentFormViewModel);

        Task<List<ShipmentFormViewModel>> ShipmentsByMemberId(int id);

        Task<ShipmentFormViewModel> ShipmentById(int id);

        Task<List<Shipment>> ShipmentsWithBooksByMemberId(int id);

        public Task<int> DeleteShipment(int id);

        public Task<int> ConfirmShipment(int id);
    }
}
