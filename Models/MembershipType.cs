using System.ComponentModel.DataAnnotations;

namespace BookSmart.Models
{
    public class MembershipType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Books Per Year")]
        public int BooksPerYear { get; set; }

        [Required]
        [Display(Name = "Discount")]
        public int DiscountPercentage { get; set; }
    }
}
