using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubsData.Domain.Entities
{
    [Table("stores")]
    public class Store
    {
        [Key]
        [Column("stor_id")]
        [StringLength(4)]
        public string StorId { get; set; }

        [Column("stor_name")]
        [StringLength(40)]
        public string? StorName { get; set; }

        [Column("stor_address")]
        [StringLength(40)]
        public string? StorAddress { get; set; }

        [Column("city")]
        [StringLength(20)]
        public string? City { get; set; }

        [Column("state")]
        [StringLength(2)]
        public string? State { get; set; }

        [Column("zip")]
        [StringLength(5)]
        public string? Zip { get; set; }
    }
}
