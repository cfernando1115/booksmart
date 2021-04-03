using BookSmart.Interfaces;
using BookSmart.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
