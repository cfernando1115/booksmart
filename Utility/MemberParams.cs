using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Utility
{
    public class MemberParams : Pagination
    {
        public string Filter { get; set; } = "none";
    }
}
