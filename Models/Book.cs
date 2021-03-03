using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        public Genre Genre { get; set; }

        [Required]
        [DisplayName("Genre")]

        public int GenreId { get; set; }
    }
}
