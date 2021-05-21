﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.Utility
{
    public class Pagination
    {
        private const int MaxPageSize = 10;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize)
                ? MaxPageSize
                : value;
        }
    }
}
