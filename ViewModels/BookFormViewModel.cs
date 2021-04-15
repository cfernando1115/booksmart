using System.Collections.Generic;
using BookSmart.Models;

namespace BookSmart.ViewModels
{
    public class BookFormViewModel
    {
        public Book Book { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}
