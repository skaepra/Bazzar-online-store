namespace Online_Store.ResponseDto.Order
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public double DropoffLatitude { get; set; }
        public double DropoffLongitude { get; set; }
        public int PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentStatus { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
    }
}
