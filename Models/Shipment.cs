using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmart.Models
{
    public class Shipment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ShipDate { get; set; }

        [Required]
        public bool IsConfirmed { get; set; } = false;

        public int? AdminId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [Required]
        [DisplayName("Book")]
        public int BookId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        [Required]
        [DisplayName("Member")]
        public int MemberId { get; set; }
    }
}
