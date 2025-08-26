using System.ComponentModel.DataAnnotations;

namespace PubsData.Domain.Entities
{
    public class Title
    {
        [Required]
        [RegularExpression(@"^[A-Z]{2}\d{4}$", ErrorMessage = "TitleId must be 2 letters followed by 4 digits (e.g., PS7777).")]
        [StringLength(6)]
        public string TitleId { get; set; } = default!;

        [Required]
        [StringLength(80)]
        public string Name { get; set; } = default!; 

        [Required]
        [StringLength(12)]
        public string Type { get; set; } = default!;

        [StringLength(4)]
        [RegularExpression(@"^(1389|0736|0877|1622|1756|99\d\d)?$", ErrorMessage = "Publisher Id must match an existing publisher.")]
        public string? PubId { get; set; }

        [Range(0, 1000000, ErrorMessage = "Price must be >= 0.")]
        public decimal? Price { get; set; }

        [Range(0, 1000000, ErrorMessage = "Advance must be >= 0.")]
        public decimal? Advance { get; set; }

        [Range(0, 100, ErrorMessage = "Royalty must be between 0–100.")]
        public int? Royalty { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "YTD Sales must be >= 0.")]
        public int? YtdSales { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PubDate { get; set; }
    }
}
