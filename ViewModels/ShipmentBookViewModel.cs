using BookSmart.Models;

namespace BookSmart.ViewModels
{
    public class ShipmentBookViewModel
    {
        public int ShipId { get; set; }

        public string ShipDate { get; set; }

        public Book Book { get; set; }
    }
}
