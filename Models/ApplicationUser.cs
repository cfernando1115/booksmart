﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
