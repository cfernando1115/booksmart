using BookSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.ViewModels
{
    public class MemberBooksViewModel
    {
        public Member Member { get; set; }

        public List<Book> Books { get; set; }
    }
}
