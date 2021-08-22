using BookSmart.Models;
using System.Collections.Generic;

namespace BookSmart.ViewModels
{
    public class BookFormViewModel
    {
        public Book Book { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}
