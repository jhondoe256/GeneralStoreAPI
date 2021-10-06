using System.Threading.Tasks;
using GeneralStoreAPI.Data;
using GeneralStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController:Controller
    {
        private GeneralStoreDBContext _context;
        public ProductController(GeneralStoreDBContext context)
        {
            _context=context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductEdit model)
        {
            Product product =new Product
            {
                Name=model.Name,
                QuantityInStock=model.Quantity,
                Price=model.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok();
        } 
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products= await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}