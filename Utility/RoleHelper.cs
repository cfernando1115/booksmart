using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Utility
{
    public static class RoleHelper
    {
        public static string Admin = "Admin";
        public static string Member = "Member";

        public static List<SelectListItem> GetRolesDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=RoleHelper.Admin, Text=RoleHelper.Admin},
                new SelectListItem{Value=RoleHelper.Member, Text=RoleHelper.Member}
            };
        }
    }
}
