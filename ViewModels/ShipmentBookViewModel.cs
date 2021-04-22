using BookSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.ViewModels
{
    public class ShipmentBookViewModel
    {
        public string ShipDate { get; set; }

        public Book Book { get; set; }
    }
}
