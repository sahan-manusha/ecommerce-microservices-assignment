using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Description = "High-performance gaming laptop", Price = 1299.99m, Category = "Electronics" },
            new Product { Id = 2, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 29.99m, Category = "Accessories" },
            new Product { Id = 3, Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 89.99m, Category = "Accessories" }
        };

        /// <summary>
        /// Retrieves all products
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_products);
        }

        /// <summary>
        /// Retrieves a product by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(new { message = $"Product with ID {id} not found" });
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Product> Update(int id, [FromBody] Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null)
                return NotFound(new { message = $"Product with ID {id} not found" });

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Category = product.Category;
            return Ok(existing);
        }

        /// <summary>
        /// Deletes a product by ID
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(new { message = $"Product with ID {id} not found" });

            _products.Remove(product);
            return NoContent();
        }
    }
}
