using BookSmart.Data;
using BookSmart.Interfaces;
using BookSmart.Models;
using Microsoft.EntityFrameworkCore;
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
                .Include(m => m.Books)
                .ThenInclude(b => b.Genre)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }
    }
}
