using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubsData.Domain.Entities
{
    public class Sales
    {
        [Required(ErrorMessage = "Store Id is required")]
        [StringLength(4, ErrorMessage = "Store Id must be exactly 4 characters")]
        [RegularExpression(@"^[A-Za-z0-9]{4}$", ErrorMessage = "Store Id must be alphanumeric (4 chars)")]
        public string StorId { get; set; } = default!;

        [Required(ErrorMessage = "Order number is required")]
        [StringLength(20, ErrorMessage = "Order number cannot exceed 20 characters")]
        public string OrdNum { get; set; } = default!;

        [Required(ErrorMessage = "Title Id is required")]
        [StringLength(6, ErrorMessage = "Title Id must be 6 characters")]
        [RegularExpression(@"^[A-Za-z0-9]{6}$", ErrorMessage = "Title Id must be alphanumeric (6 chars)")]
        public string TitleId { get; set; } = default!;

        [Required(ErrorMessage = "Order date is required")]
        [DataType(DataType.Date)]
        public DateTime OrdDate { get; set; }

        [Range(1, 32767, ErrorMessage = "Quantity must be between 1 and 32767")]
        public short Qty { get; set; }

        [Required(ErrorMessage = "Payterms are required")]
        [StringLength(12, ErrorMessage = "Payterms cannot exceed 12 characters")]
        public string Payterms { get; set; } = default!;

        [NotMapped]
        public Title? Title { get; set; }

        [NotMapped]
        public Store? Store { get; set; }

        public string? TitleName { get; set; }
    }
}
