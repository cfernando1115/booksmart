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

        public string Name { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public Genre Genre { get; set; }

        [DisplayName("Genre")]

        public int GenreId { get; set; }
    }
}
