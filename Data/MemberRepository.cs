using BookSmart.Interfaces;
using BookSmart.Models;
using BookSmart.Utility;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Data
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
            : base(context) 
        {
            _context = context;
        }


        public async Task<Member> GetMemberByUsernameAsync(string username)
        {
            return await _context.Members.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<Member> GetMemberByUsernameWithBooksAsync(string username)
        {
            return await _context.Members
                .Include(m => m.Books)
                .ThenInclude(b => b.Genre)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }


        public async Task<Member> GetMemberByIdWithBooksAsync(int id)
        {
            return await _context.Members
                .Include(m => m.Books)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedList<Member>> GetMembersWithMembershipTypeAsync(MemberParams memberParams)
        {
            var query = _context.Members
                .Include(m => m.MembershipType);

            return await PagedList<Member>.CreateAsync(query, memberParams.PageNumber, memberParams.PageSize);
        }

        public async Task<Member> GetMemberWithMembershipTypeAsync(int id)
        {
            return await _context.Members
                .Include(m => m.MembershipType)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public IQueryable<Member> GetMembers()
        {
            return _context.Members.AsQueryable();
        }

        public IQueryable<Member> GetMember(int id)
        {
            return _context.Members.Where(m => m.Id == id).AsQueryable();
        }

        public async Task<Member> GetMemberWithShipmentsAsync(int id)
        {
            return await _context.Members
                .Include(m => m.Shipments)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
