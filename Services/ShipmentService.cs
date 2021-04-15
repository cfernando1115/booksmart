using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public class ShipmentService : ShipmentRepository, IShipmentService
    {
        public ShipmentService(ApplicationDbContext context)
            : base(context) { }

        public async Task<int> AddUpdateAsync(ShipmentFormViewModel shipmentFormViewModel)
        {
            var shipDate = DateTime.Parse(shipmentFormViewModel.ShipDate);

            if (shipmentFormViewModel != null && shipmentFormViewModel.Id > 0)
            {

            }
            else
            {
                Shipment shipment = new Shipment
                {
                    ShipDate = shipDate,
                    BookId = JsonSerializer.Deserialize<int>(shipmentFormViewModel.BookId),
                    MemberId = JsonSerializer.Deserialize<int>(shipmentFormViewModel.MemberId)
                };

                ApplicationDbContext.Shipments.Add(shipment);
                await ApplicationDbContext.SaveChangesAsync();
                return 2;
            }

            return 0;
        }

        public List<ShipmentFormViewModel> ShipmentsByMemberId(int id)
        {
            return ApplicationDbContext.Shipments.Where(s => s.MemberId == id)
                .ToList()
                .Select(s => new ShipmentFormViewModel
                {
                    Id = s.Id,
                    BookId = s.BookId.ToString(),
                    ShipDate = s.ShipDate.ToString("yyyy-MM-dd"),
                    IsConfirmed = s.IsConfirmed,
                    BookName = ApplicationDbContext.Books.Where(b => b.Id == s.BookId).Select(b => b.Name).FirstOrDefault()
                }).ToList();
        }

        public ShipmentFormViewModel ShipmentById(int id)
        {
            return ApplicationDbContext.Shipments.Where(s => s.Id == id)
                .ToList()
                .Select(s => new ShipmentFormViewModel
                {
                    Id = s.Id,
                    BookId = s.BookId.ToString(),
                    ShipDate = s.ShipDate.ToString("yyyy-MM-dd"),
                    IsConfirmed = s.IsConfirmed,
                    BookName = ApplicationDbContext.Books.Where(b => b.Id == s.BookId).Select(b => b.Name).FirstOrDefault()
                }).SingleOrDefault();
        }
    }
}
