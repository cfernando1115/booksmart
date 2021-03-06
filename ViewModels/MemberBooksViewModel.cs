﻿using BookSmart.Models;
using System.Collections.Generic;

namespace BookSmart.ViewModels
{
    public class MemberBooksViewModel
    {
        public Member Member { get; set; }

        public List<Book> Books { get; set; }
    }
}
