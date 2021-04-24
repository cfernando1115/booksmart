using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Services
{
    public class MemberService : MemberRepository, IMemberService
    {
        public MemberService(ApplicationDbContext context)
            : base(context) { }

        public async Task<Member> GetMemberByUsernameWithBooksAndShipmentsAsync(string username)
        {
            return await ApplicationDbContext.Members
                .Include(m => m.Shipments)
                .Include(m => m.MembershipType)
                .Include(m => m.Books)
                .ThenInclude(b => b.Genre)
                .SingleOrDefaultAsync(u => u.UserName == username);
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

        public async Task<int> AddToBagAsync(Member member, int bookId)
        {
            var bookToAdd = await ApplicationDbContext.Books.FindAsync(bookId);

            if (member.Books.Contains(bookToAdd))
            {
                return 2;
            }

            member.Books.Add(bookToAdd);
            await ApplicationDbContext.SaveChangesAsync();
            return 1;
        }

        public async Task<int> RemoveFromBagAsync(Member member, int bookId)
        {
            var bookToRemove = await ApplicationDbContext.Books.FindAsync(bookId);

            if (member.Books.Contains(bookToRemove))
            {
                member.Books.Remove(bookToRemove);

                var shipment = member.Shipments.FirstOrDefault(s => s.BookId == bookToRemove.Id);
                if (shipment != null)
                {
                    member.Shipments.Remove(shipment);
                    ApplicationDbContext.Shipments.Remove(shipment);

                    await ApplicationDbContext.SaveChangesAsync();
                    return 1;
                }
            }
            return 2;
        }

    }
}
