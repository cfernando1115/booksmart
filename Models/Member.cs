﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Models
{
    public class Member : ApplicationUser
    {
        public int BooksRemaining { get; set; }

        public List<Book> Books { get; set; }

        public DateTime? LastLogin { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public int? MembershipTypeId { get; set; }
    }
}
