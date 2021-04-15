using BookSmart.Models;
using System.Collections.Generic;

namespace BookSmart.ViewModels
{
    public class MemberUpdateViewModel
    {
        public Member Member { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}
