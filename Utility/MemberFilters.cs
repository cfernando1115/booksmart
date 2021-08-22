using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookSmart.Utility
{
    public static class MemberFilters
    {
        public static string MembershipType = "membershiptype";
        public static string LastLogin = "lastlogin";
        public static string BooksRemaining = "booksremaining";

        public static List<SelectListItem> GetMemberFilters()
        {
            return new List<SelectListItem>
            {
                 new SelectListItem{Value=MemberFilters.MembershipType, Text="Membership Type"},
                 new SelectListItem{Value=MemberFilters.LastLogin, Text="Last Login"},
                 new SelectListItem{Value=MemberFilters.BooksRemaining, Text="Books Remaining"}
            };
        }
    }

}
