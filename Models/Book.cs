﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string Description { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        [DisplayName("Genre")]
        public int GenreId { get; set; }

        public ICollection<Member> Members { get; set; }

    }
}
