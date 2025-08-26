using System.ComponentModel.DataAnnotations;

namespace PubsData.Domain.Entities
{
    public class Author
    {
        [Key]
        [Required]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage = "Author ID must be in format ###-##-####")]
        public string AuId { get; set; } = default!;

        [Required]
        [StringLength(40)]
        public string AuLName { get; set; } = default!;

        [Required]
        [StringLength(20)]
        public string AuFName { get; set; } = default!;

        [Required]
        [RegularExpression(@"^\d{3} \d{3}-\d{4}$", ErrorMessage = "Phone must be in format ### ###-####")]
        public string Phone { get; set; } = default!;

        [StringLength(40)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? City { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters.")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "State must be two uppercase letters.")]
        public string? State { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessage = "Zip must be exactly 5 digits.")]
        public string? Zip { get; set; }

        [Required]
        public bool Contract { get; set; }
    }
}
