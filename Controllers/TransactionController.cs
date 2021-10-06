using System;
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
    public class TransactionController : Controller
    {
        private GeneralStoreDBContext _context;
        public TransactionController(GeneralStoreDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromForm] TransactionEdit model)
        {
            var transaction = new Transaction
            {
                ProductId=model.ProductId,
                CustomerId=model.CustomerId,
                DateOfTransaction=DateTime.UtcNow
            };

            var product = await _context.Products.SingleOrDefaultAsync(p=>p.Id==transaction.ProductId);

            if (product is null)
            {
                return NotFound();
            }

            if (product.QuantityInStock>0)
            {
                product.QuantityInStock-=model.Quantity;
                model.TotalCost= Convert.ToDecimal(model.Quantity *product.Price);
            }

            var customer= await _context.Customers.SingleOrDefaultAsync(c=>c.Id==transaction.CustomerId);

            if (customer is null)
            {
                return NotFound();
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet]
        public async Task<IActionResult>GetAllTransactions()
        {
            var transactions =  _context.Transactions.Select(t=>new TransactionListItem
            {
                TransactionId=t.Id,
                // CustomerId=t.CustomerId,
                CustomerName= _context.Customers.FirstOrDefault(c=>c.Id==t.CustomerId).Name,
                // ProductId=t.ProductId,
               ProductName=_context.Products.FirstOrDefault(c=>c.Id==t.ProductId).Name
            });

            await transactions.ToListAsync();
            return Ok(transactions);
        }   
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTransaction([FromForm]TransactionEdit model, [FromRoute] int id)
        {
            if (id<1 || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldData=await _context.Transactions.SingleOrDefaultAsync(t=>t.Id==id);
            if(oldData is null)
            {
                return NotFound();
            }
            oldData.CustomerId=model.CustomerId;
            oldData.ProductId=model.ProductId;
            oldData.DateOfTransaction=DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute]int id)
        {
            var oldData=await _context.Transactions.FindAsync(id);
            if (oldData is null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(oldData);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}