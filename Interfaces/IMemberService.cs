using BookSmart.Models;
using BookSmart.ViewModels;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IMemberService : IMemberRepository
    {
        Task<Member> GetMemberByUsernameWithBooksAndShipmentsAsync(string username);

        MemberBagViewModel BuildMemberBag(Member member);

        Task<int> AddToBagAsync(Member member, int bookId);

        Task<int> RemoveFromBagAsync(Member member, int bookId);
    }
}
