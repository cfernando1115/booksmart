using BookSmart.Models;
using BookSmart.ViewModels;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetMemberByUsernameWithBooksAndShipmentsAsync(string username);

        MemberBagViewModel BuildMemberBag(Member member);

        int AddToBag(Member member, int bookId);

        int RemoveFromBag(Member member, int bookId);

        Task<ShipmentViewModel> GetMemberShipmentsModel(int id);
    }
}
