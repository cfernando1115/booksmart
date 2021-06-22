using BookSmart.Models;
using BookSmart.Utility;
using Repository;
using System.Collections.Generic;
using System.Linq;
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

        Task<Member> GetMemberWithShipmentsAsync(int id);

        IQueryable<Member> GetMembers();

        IQueryable<Member> GetMember(int id);
    }
}
