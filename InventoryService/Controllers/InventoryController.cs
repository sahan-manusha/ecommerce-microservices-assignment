using Microsoft.AspNetCore.Mvc;
using InventoryService.Models;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private static readonly List<InventoryItem> _inventory = new()
        {
            new InventoryItem { Id = 1, ProductId = 1, Quantity = 50, Warehouse = "Warehouse A - Colombo", LastUpdated = DateTime.Now.AddDays(-1) },
            new InventoryItem { Id = 2, ProductId = 2, Quantity = 200, Warehouse = "Warehouse B - Kandy", LastUpdated = DateTime.Now.AddDays(-3) },
            new InventoryItem { Id = 3, ProductId = 3, Quantity = 75, Warehouse = "Warehouse A - Colombo", LastUpdated = DateTime.Now }
        };

        /// <summary>
        /// Retrieves all inventory items
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<InventoryItem>> GetAll()
        {
            return Ok(_inventory);
        }

        /// <summary>
        /// Retrieves an inventory item by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<InventoryItem> GetById(int id)
        {
            var item = _inventory.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound(new { message = $"Inventory item with ID {id} not found" });
            return Ok(item);
        }

        /// <summary>
        /// Creates a new inventory item
        /// </summary>
        [HttpPost]
        public ActionResult<InventoryItem> Create([FromBody] InventoryItem item)
        {
            item.Id = _inventory.Any() ? _inventory.Max(i => i.Id) + 1 : 1;
            item.LastUpdated = DateTime.Now;
            _inventory.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates an existing inventory item
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<InventoryItem> Update(int id, [FromBody] InventoryItem item)
        {
            var existing = _inventory.FirstOrDefault(i => i.Id == id);
            if (existing == null)
                return NotFound(new { message = $"Inventory item with ID {id} not found" });

            existing.ProductId = item.ProductId;
            existing.Quantity = item.Quantity;
            existing.Warehouse = item.Warehouse;
            existing.LastUpdated = DateTime.Now;
            return Ok(existing);
        }

        /// <summary>
        /// Deletes an inventory item by ID
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _inventory.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound(new { message = $"Inventory item with ID {id} not found" });

            _inventory.Remove(item);
            return NoContent();
        }
    }
}
