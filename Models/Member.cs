﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmart.Models
{
    public class Member : ApplicationUser
    {
        public int BooksRemaining { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

        public DateTime SignUpDate { get; set; }

        public DateTime MembershipExpiration { get; set; }

        public DateTime? LastLogin { get; set; }

        [ForeignKey("MembershipTypeId")]
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public int? MembershipTypeId { get; set; }
    }
}
