using BookSmart.Models;
using BookSmart.Utility;
using System.Collections.Generic;

namespace BookSmart.ViewModels
{
    public class BookFilterViewModel
    {
        public PagedList<Book> Books { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public int? GenreFilter { get; set; }
    }
}
