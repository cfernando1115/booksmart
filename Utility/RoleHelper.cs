using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookSmart.Utility
{
    public static class RoleHelper
    {
        public static string Admin = "Admin";
        public static string Member = "Member";

        public static List<SelectListItem> GetRolesDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=RoleHelper.Admin, Text=RoleHelper.Admin},
                    new SelectListItem{Value=RoleHelper.Member, Text=RoleHelper.Member}
                };

            }

            return new List<SelectListItem>
            {
                new SelectListItem{Value=RoleHelper.Member, Text=RoleHelper.Member}
            };
        }
    }
}
