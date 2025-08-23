using System.Threading.Tasks;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace AtoZ.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Client _supabase;
        public ProductController(Client supabase)
        {
            _supabase = supabase;
        }
        // public static Product product = new();

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDto request)
        {
            if (request.Name == "") return BadRequest("Name cannot be empty!");
            if (request.Price <= 0) return BadRequest("Product price cannot be empty or <= 0");
            // product.Name = request.Name;
            // product.Price = request.Price;
            // product.Description = request.Description;

            var data = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };

            var response = await _supabase.From<Product>().Insert(data);

            if (response.Models != null && response.Models.Any())
            {
                var insertedRecord = response.Models.First();
                Console.WriteLine("==========================INSERTION RESPONSE =======================");
                Console.WriteLine(insertedRecord.Id);
            }
            
            return Ok(data);
        }
    }
}
