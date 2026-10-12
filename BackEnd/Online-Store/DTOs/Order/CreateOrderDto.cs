using System.ComponentModel.DataAnnotations;
using Online_Store.DTOs;

namespace Online_Store.DTOs.Order
{
    public class CreateOrderDto
    {
        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required, Phone, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Building { get; set; } = string.Empty;

        public double DropoffLatitude { get; set; }

        public double DropoffLongitude { get; set; }

        public int PaymentMethod { get; set; }

        public string? Notes { get; set; }

        [Required, MinLength(1, ErrorMessage = "يجب إضافة عنصر واحد على الأقل في الطلب")]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
