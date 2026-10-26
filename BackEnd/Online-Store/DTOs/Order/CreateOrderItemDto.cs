using System.ComponentModel.DataAnnotations;

namespace Online_Store.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string Size { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من 0")]
        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "الكمية يجب أن تكون 1 على الأقل")]
        public int Quantity { get; set; }
    }
}