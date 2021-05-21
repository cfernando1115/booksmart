using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.Utility;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationDbContext context)
            : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<Member> GetMemberByUsernameAsync(string username)
        {
            return await ApplicationDbContext.Members.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<Member> GetMemberByUsernameWithBooksAsync(string username)
        {
            return await ApplicationDbContext.Members
                .Include(m => m.Books)
                .ThenInclude(b => b.Genre)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }


        public async Task<Member> GetMemberByIdWithBooksAsync(int id)
        {
            return await ApplicationDbContext.Members
                .Include(m => m.Books)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedList<Member>> GetMembersWithMembershipTypeAsync(MemberParams memberParams)
        {
            var query = ApplicationDbContext.Members
                .Include(m => m.MembershipType);

            return await PagedList<Member>.CreateAsync(query, memberParams.PageNumber, memberParams.PageSize);
        }

        public async Task<Member> GetMemberWithMembershipTypeAsync(int id)
        {
            return await ApplicationDbContext.Members
                .Include(m => m.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

    }
}
