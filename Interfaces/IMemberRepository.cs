using BookSmart.Models;
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

        Task<IEnumerable<Member>> GetMembersWithMembershipTypeAsync();

        Task<Member> GetMemberWithMembershipTypeAsync(int id);
    }
}
