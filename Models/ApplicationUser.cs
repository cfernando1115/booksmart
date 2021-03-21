using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }

        [Display(Name="Membership Type")]
        public int? MembershipTypeId { get; set; }
    }
}
