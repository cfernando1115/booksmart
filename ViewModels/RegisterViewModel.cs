﻿using BookSmart.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSmart.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Does not match password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [Display(Name = "Membership Type")]
        public int? MembershipTypeId { get; set; }

        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}
