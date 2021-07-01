using BookSmart.Models;
using BookSmart.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.ViewModels
{
    public class MemberFilterViewModel
    {
        public PagedList<Member> Members { get; set; }

        public IEnumerable<SelectListItem> MemberFilters { get; set; }
    }
}
