using BookSmart.Data;
using BookSmart.Extensions;
using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public class MemberService : IMemberService
    {

        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Member> GetMemberByUsernameWithBooksAndShipmentsAsync(string username)
        {
            var members = _unitOfWork.Members.GetMembers();

            return await members
                .Include(m => m.Shipments)
                .Include(m => m.MembershipType)
                .Include(m => m.Books)
                .ThenInclude(b => b.Genre)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<ShipmentViewModel> GetMemberShipmentsModel(int id)
        {
            var memberShipments = await _unitOfWork.Shipments.GetShipments()
                .Where(s => s.MemberId == id && (s.ShipDate != null && s.ShipDate < DateTime.Now) || s.IsConfirmed)
                .ResetUnconfirmedPastShipments(_unitOfWork)
                .Select(b => b.BookId)
                .ToListAsync();

            return await _unitOfWork.Members.GetMember(id)
                .Include(m => m.Books)
                .Select(m => new ShipmentViewModel
                {
                    Member = m,
                    Books = m.Books.Where(b => !memberShipments.Contains(b.Id))
                }).FirstOrDefaultAsync();
        }

        public MemberBagViewModel BuildMemberBag(Member member)
        {
            return new MemberBagViewModel
            {
                Member = member,
                ConfirmedShipments = member.Shipments?.Where(s => s.IsConfirmed == true)
                    .Select(b => new ShipmentBookViewModel { Book = b.Book, ShipDate = b.ShipDate.ToString("yyyy-MM-dd") })
                    .ToList(),
                UnconfirmedShipments = member.Shipments?.Where(s => s.IsConfirmed == false && s.ShipDate != null)
                    .Select(b => new ShipmentBookViewModel { Book = b.Book, ShipDate = b.ShipDate.ToString("yyyy-MM-dd") })
                    .ToList(),
                UnscheduledBooks = member.Books?.Where(b => member.Shipments
                    .SingleOrDefault(s => s.BookId == b.Id) == null)
                    .ToList(),
                DiscountRate = 1 - member.MembershipType.DiscountPercentage / 100f
            };
        }

        public int AddToBag(Member member, int bookId)
        {
            var bookToAdd = _unitOfWork.Books.Get(bookId);

            if (member.Books.Contains(bookToAdd))
            {
                return 2;
            }

            member.Books.Add(bookToAdd);

            return 1;
        }

        public int RemoveFromBag(Member member, int bookId)
        {
            var bookToRemove = _unitOfWork.Books.Get(bookId);

            if (member.Books.Contains(bookToRemove))
            {
                member.Books.Remove(bookToRemove);

                var shipment = member.Shipments.FirstOrDefault(s => s.BookId == bookToRemove.Id);
                if (shipment != null)
                {
                    member.Shipments.Remove(shipment);
                    _unitOfWork.Shipments.Remove(shipment);
                }

                return 1;
            }
            return 2;
        }

    }
}
