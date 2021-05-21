using BookSmart.Models;
using BookSmart.Utility;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member> GetMemberByUsernameAsync(string username);

        Task<Member> GetMemberByUsernameWithBooksAsync(string username);

        Task<Member> GetMemberByIdWithBooksAsync(int id);

        Task<PagedList<Member>> GetMembersWithMembershipTypeAsync(MemberParams memberParams);

        Task<Member> GetMemberWithMembershipTypeAsync(int id);
    }
}
