using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookSmart.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
