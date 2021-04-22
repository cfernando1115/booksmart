using BookSmart.Models;
using System.Collections.Generic;

namespace BookSmart.ViewModels
{
    public class MemberBagViewModel
    {
        public Member Member { get; set; }

        public List<Book> UnscheduledBooks { get; set; }

        public List<ShipmentBookViewModel> ConfirmedShipments { get; set; }

        public List<ShipmentBookViewModel> UnconfirmedShipments { get; set; }
    }
}
