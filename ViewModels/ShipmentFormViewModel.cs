
namespace BookSmart.ViewModels
{
    public class ShipmentFormViewModel
    {
        public int? Id { get; set; }

        public string ShipDate { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public string AdminId { get; set; }

        public string BookId { get; set; }

        public string MemberId { get; set; }

        public string BookName { get; set; }

        public string MemberName { get; set; }

        public string AdminName { get; set; }
    }
}
