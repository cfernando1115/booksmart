using BookSmart.Dtos;
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
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShipmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddUpdateAsync(ShipmentFormDto shipmentFormDto)
        {
            var shipDate = DateTime.Parse(shipmentFormDto.ShipDate);

            if (shipmentFormDto != null && shipmentFormDto.Id > 0)
            {
                var shipment = await _unitOfWork.Shipments.GetShipmentAsync((int)(shipmentFormDto.Id));

                if (shipment != null)
                {
                    shipment.ShipDate = shipDate;
                    shipment.IsConfirmed = shipmentFormDto.IsConfirmed;
                    return 1;
                }
            }
            else
            {
                Shipment shipment = new Shipment
                {
                    ShipDate = shipDate,
                    BookId = JsonSerializer.Deserialize<int>(shipmentFormDto.BookId),
                    MemberId = JsonSerializer.Deserialize<int>(shipmentFormDto.MemberId)
                };

                var member = await _unitOfWork.Members.GetMemberWithShipmentsAsync(Convert.ToInt32(shipmentFormDto.MemberId));
                member?.Shipments.Add(shipment);
                _unitOfWork.Shipments.Add(shipment);
                return 2;
            }

            return 0;
        }

        public async Task<List<ShipmentFormDto>> ShipmentsByMemberId(int id)
        {
            var shipments = _unitOfWork.Shipments.GetShipments();

            return await shipments.Include(b => b.Book).Where(s => s.MemberId == id)
                .Select(s => new ShipmentFormDto
                {
                    Id = s.Id,
                    BookId = s.BookId.ToString(),
                    ShipDate = s.ShipDate.ToString("yyyy-MM-dd"),
                    IsConfirmed = s.IsConfirmed,
                    BookName = _unitOfWork.Books.GetBooks()
                        .Where(b => b.Id == s.BookId)
                        .Select(b => b.Name)
                        .FirstOrDefault()
                }).ToListAsync();
        }

        public async Task<List<Shipment>> ShipmentsWithBooksByMemberId(int id)
        {
            return await _unitOfWork.Shipments.GetShipments()
                .Include(s => s.Book)
                .ThenInclude(b => b.Genre)
                .Where(s => s.MemberId == id)
                .ToListAsync();
        }

        public async Task<ShipmentFormDto> ShipmentById(int id)
        {
            return await _unitOfWork.Shipments.GetShipment(id)
                .Select(s => new ShipmentFormDto
                {
                    Id = s.Id,
                    BookId = s.BookId.ToString(),
                    ShipDate = s.ShipDate.ToString("yyyy-MM-dd"),
                    IsConfirmed = s.IsConfirmed,
                    BookName = _unitOfWork.Books.GetBooks()
                        .Where(b => b.Id == s.BookId)
                        .Select(b => b.Name)
                        .FirstOrDefault()
                }).SingleOrDefaultAsync();
        }

        public async Task<int> DeleteShipment(int id)
        {
            var shipment = await _unitOfWork.Shipments.GetShipmentAsync(id);
            if (shipment != null)
            {
                _unitOfWork.Shipments.Remove(shipment);
                var member = await _unitOfWork.Members.GetMemberWithShipmentsAsync(shipment.MemberId);
                member?.Shipments.Remove(shipment);
                return 1;
            }
            return 0;
        }

        public async Task<int> ConfirmShipment(int id)
        {
            var shipment = await _unitOfWork.Shipments.GetShipmentAsync(id);
            if (shipment != null)
            {
                shipment.IsConfirmed = true;
                var member = await _unitOfWork.Members.GetMemberWithMembershipTypeAsync(shipment.MemberId);
                if (member.MembershipType.Id != 1)
                {
                    if (member.BooksRemaining == 0)
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
