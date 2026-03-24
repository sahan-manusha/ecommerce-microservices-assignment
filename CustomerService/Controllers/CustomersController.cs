using Microsoft.AspNetCore.Mvc;
using CustomerService.Models;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private static readonly List<Customer> _customers = new()
        {
            new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", Phone = "+94771234567", Address = "123 Main Street, Colombo" },
            new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", Phone = "+94779876543", Address = "456 Oak Avenue, Kandy" },
            new Customer { Id = 3, FirstName = "Bob", LastName = "Johnson", Email = "bob.johnson@email.com", Phone = "+94775551234", Address = "789 Pine Road, Galle" }
        };

        /// <summary>
        /// Retrieves all customers
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(_customers);
        }

        /// <summary>
        /// Retrieves a customer by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound(new { message = $"Customer with ID {id} not found" });
            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            customer.Id = _customers.Any() ? _customers.Max(c => c.Id) + 1 : 1;
            _customers.Add(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Customer> Update(int id, [FromBody] Customer customer)
        {
            var existing = _customers.FirstOrDefault(c => c.Id == id);
            if (existing == null)
                return NotFound(new { message = $"Customer with ID {id} not found" });

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.Address = customer.Address;
            return Ok(existing);
        }

        /// <summary>
        /// Deletes a customer by ID
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound(new { message = $"Customer with ID {id} not found" });

            _customers.Remove(customer);
            return NoContent();
        }
    }
}
