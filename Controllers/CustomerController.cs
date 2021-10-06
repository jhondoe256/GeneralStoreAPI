using System.Linq;
using System.Threading.Tasks;
using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController:Controller
    {
        private GeneralStoreDBContext _context;
        public CustomerController(GeneralStoreDBContext context)
        {
            _context=context;
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromForm] CustomerEdit model)
        {
            var customer = new Customer
            {
                Name=model.Name,
                Email=model.Email
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
                var customers = await _context.Customers.Include(c=>c.Transactions).Select(c=>new CustomerListItem
                {
                    CustomerId=c.Id,
                    Transactions=_context.Transactions.Where(t=>t.CustomerId==c.Id).ToList(),
                    
                }).ToListAsync();
                return Ok(customers);
        }
    }
}