using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new()
        {
            new Order { Id = 1, CustomerId = 1, ProductId = 1, Quantity = 1, TotalPrice = 1299.99m, Status = "Completed", OrderDate = DateTime.Now.AddDays(-5) },
            new Order { Id = 2, CustomerId = 2, ProductId = 2, Quantity = 2, TotalPrice = 59.98m, Status = "Processing", OrderDate = DateTime.Now.AddDays(-2) },
            new Order { Id = 3, CustomerId = 1, ProductId = 3, Quantity = 1, TotalPrice = 89.99m, Status = "Pending", OrderDate = DateTime.Now }
        };

        /// <summary>
        /// Retrieves all orders
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(_orders);
        }

        /// <summary>
        /// Retrieves an order by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });
            return Ok(order);
        }

        /// <summary>
        /// Creates a new order
        /// </summary>
        [HttpPost]
        public ActionResult<Order> Create([FromBody] Order order)
        {
            order.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            order.OrderDate = DateTime.Now;
            _orders.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Updates an existing order
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Order> Update(int id, [FromBody] Order order)
        {
            var existing = _orders.FirstOrDefault(o => o.Id == id);
            if (existing == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            existing.CustomerId = order.CustomerId;
            existing.ProductId = order.ProductId;
            existing.Quantity = order.Quantity;
            existing.TotalPrice = order.TotalPrice;
            existing.Status = order.Status;
            return Ok(existing);
        }

        /// <summary>
        /// Deletes an order by ID
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            _orders.Remove(order);
            return NoContent();
        }
    }
}
