using BookSmart.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IShipmentService : IShipmentRepository
    {
        Task<int> AddUpdateAsync(ShipmentFormViewModel shipmentFormViewModel);

        List<ShipmentFormViewModel> ShipmentsByMemberId(int id);

        ShipmentFormViewModel ShipmentById(int id);

        public Task<int> DeleteShipment(int id);

        public Task<int> ConfirmShipment(int id);
    }
}
