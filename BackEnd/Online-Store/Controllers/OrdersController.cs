using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.DTOs.Order;
using Online_Store.Entities;
using Online_Store.ResponseDto.Order;
using Online_Store.DTOs;


namespace Online_Store.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => MapToResponseDto(o))
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("order/{id:guid}")]
        public async Task<ActionResult<OrderResponseDto>> GetOrderById(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { message = "الطلب غير موجود" });
            }

            return Ok(MapToResponseDto(order));
        }

        // POST: api/orders
        [HttpPost("createOrder")]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // إنشاء كيان الطلب الرئيسي
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Country = dto.Country,
                City = dto.City,
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email,
                Building = dto.Building,
                DropoffLatitude = dto.DropoffLatitude,
                DropoffLongitude = dto.DropoffLongitude,
                PaymentMethod = dto.PaymentMethod,
                Notes = dto.Notes,
                PaymentStatus = 0, // مثلاً: 0 = Pending
                Status = 0,        // مثلاً: 0 = New / Processing
                CreatedAt = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            // حساب مجاميع كل عنصر وإضافته
            foreach (var itemDto in dto.Items)
            {
                var lineTotal = itemDto.UnitPrice * itemDto.Quantity;
                totalAmount += lineTotal;

                order.Items.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductName = itemDto.ProductName,
                    Color = itemDto.Color,
                    UnitPrice = itemDto.UnitPrice,
                    Quantity = itemDto.Quantity,
                    LineTotal = lineTotal
                });
            }

            order.TotalAmount = totalAmount;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var response = MapToResponseDto(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
        }

        // دالة مساعدة لتحويل الـ Entity إلى Response DTO
        private static OrderResponseDto MapToResponseDto(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                Country = order.Country,
                City = order.City,
                FullName = order.FullName,
                Phone = order.Phone,
                Email = order.Email,
                Building = order.Building,
                DropoffLatitude = order.DropoffLatitude,
                DropoffLongitude = order.DropoffLongitude,
                PaymentMethod = order.PaymentMethod,
                Notes = order.Notes,
                TotalAmount = order.TotalAmount,
                PaymentStatus = order.PaymentStatus,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemResponseDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Color = i.Color,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity,
                    LineTotal = i.LineTotal
                }).ToList()
            };
        }
    }
}
