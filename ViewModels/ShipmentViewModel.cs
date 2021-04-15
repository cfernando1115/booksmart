using BookSmart.Models;
using System.Collections.Generic;

namespace BookSmart.ViewModels
{
    public class ShipmentViewModel
    {
        public Member Member { get; set; }

        public IEnumerable<Book> Books { get; set; }

    }
}
