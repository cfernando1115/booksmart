﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace BookSmart.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Author { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Today;

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        [Required]
        [DisplayName("Genre")]
        public int GenreId { get; set; }

        //[ForeignKey("MemberId")]
        public ICollection<Member> Members { get; set; }

        //public List<int> MemberId { get; set; }
    }
}
