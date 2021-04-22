using BookSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Interfaces
{
    public interface IMemberService : IMemberRepository
    {
        Task<Member> GetMemberByUsernameWithBooksAndShipmentsAsync(string username);
    }
}
