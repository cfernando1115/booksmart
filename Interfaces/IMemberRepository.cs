using BookSmart.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member> GetMemberByUsernameAsync(string username);
    }
}
