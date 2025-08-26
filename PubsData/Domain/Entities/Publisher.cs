using System.ComponentModel.DataAnnotations;

namespace PubsData.Domain.Entities
{
    public class Publisher
    {
        [Key]
        [Required]
        [StringLength(4)]
        [RegularExpression(@"^(1389|0736|0877|1622|1756|99\d\d)$",ErrorMessage = "PubId must be 1389, 0736, 0877, 1622, 1756, or 99xx")]
        public string PubId { get; set; } = default!;



        [Required]
        [StringLength(40)]
        public string PubName { get; set; } = default!;

        [StringLength(20)]
        [RegularExpression(@"^[A-Za-z\s]{2,20}$", ErrorMessage = "City must be 2–20 letters")]
        public string? City { get; set; }

        [StringLength(2)]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "State must be 2 uppercase letters")]
        public string? State { get; set; }

        [StringLength(30)]
        public string? Country { get; set; } = "USA";
    }
}
