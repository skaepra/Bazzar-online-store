using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Store.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Building { get; set; } = string.Empty;

        public double DropoffLatitude { get; set; }

        public double DropoffLongitude { get; set; }

        public int PaymentMethod { get; set; }

        public string? Notes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public int PaymentStatus { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationship Navigation Property
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}