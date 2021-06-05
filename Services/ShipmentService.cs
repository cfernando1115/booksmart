using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public class ShipmentService : ShipmentRepository, IShipmentService
    {
        private readonly ApplicationDbContext _context;
        public ShipmentService(ApplicationDbContext context)
            : base(context) 
        {
            _context = context;
        }

        public async Task<int> AddUpdateAsync(ShipmentFormViewModel shipmentFormViewModel)
        {
            var shipDate = DateTime.Parse(shipmentFormViewModel.ShipDate);

            if (shipmentFormViewModel != null && shipmentFormViewModel.Id > 0)
            {
                var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == shipmentFormViewModel.Id);

                if(shipment != null)
                {
                    shipment.ShipDate = shipDate;
                    shipment.IsConfirmed = shipmentFormViewModel.IsConfirmed;
                    return 1;
                }
            }
            else
            {
                Shipment shipment = new Shipment
                {
                    ShipDate = shipDate,
                    BookId = JsonSerializer.Deserialize<int>(shipmentFormViewModel.BookId),
                    MemberId = JsonSerializer.Deserialize<int>(shipmentFormViewModel.MemberId)
                };

                var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(shipmentFormViewModel.MemberId));
                member?.Shipments.Add(shipment);
                _context.Shipments.Add(shipment);
                return 2;
            }

            return 0;
        }

        public List<ShipmentFormViewModel> ShipmentsByMemberId(int id)
        {
            return _context.Shipments.Include(b => b.Book).Where(s => s.MemberId == id)
                .ToList()
                .Select(s => new ShipmentFormViewModel
                {
                    Id = s.Id,
                    BookId = s.BookId.ToString(),
                    ShipDate = s.ShipDate.ToString("yyyy-MM-dd"),
                    IsConfirmed = s.IsConfirmed,
                    BookName = _context.Books.Where(b => b.Id == s.BookId).Select(b => b.Name).FirstOrDefault()
                }).ToList();
        }

        public async Task<List<Shipment>> ShipmentsWithBooksByMemberId(int id)
        {
            return await _context.Shipments.Include(s => s.Book).ThenInclude(b =>b.Genre).Where(s => s.MemberId == id).ToListAsync();
        }

        public ShipmentFormViewModel ShipmentById(int id)
        {
            return _context.Shipments.Where(s => s.Id == id)
                .ToList()
                .Select(s => new ShipmentFormViewModel
                {
                    Id = s.Id,
                    BookId = s.BookId.ToString(),
                    ShipDate = s.ShipDate.ToString("yyyy-MM-dd"),
                    IsConfirmed = s.IsConfirmed,
                    BookName = _context.Books.Where(b => b.Id == s.BookId).Select(b => b.Name).FirstOrDefault()
                }).SingleOrDefault();
        }

        public async Task<int> DeleteShipment(int id)
        {
            var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
            if(shipment != null)
            {
                _context.Shipments.Remove(shipment);
                var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == shipment.MemberId);
                member?.Shipments.Remove(shipment);
                return 1;
            }
            return 0;
        }

        public async Task<int> ConfirmShipment(int id)
        {
            var shipment = await _context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
            if(shipment != null)
            {
                shipment.IsConfirmed = true;
                var member = await _context.Members.Include(m => m.MembershipType)
                    .FirstOrDefaultAsync(m => m.Id == shipment.MemberId);
                if(member.MembershipType.Id != 1)
                {
                    if(member.BooksRemaining == 0)
                    {
                        return -1;
                    }
                    member.BooksRemaining--;
                }
                return 1;
            }
            return 0;
        }
    }
}
