using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookSmart.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
