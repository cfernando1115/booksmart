﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Utility
{
    public class BookParams : Pagination
    {
        public int GenreId { get; set; } = 0;
    }
}
