using System.Net;
using System.Threading.Tasks;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Supabase;

namespace AtoZ.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Client _supabase;
        public ProductsController(Client supabase)
        {
            _supabase = supabase;
        }
        // public static Product product = new();

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(ProductDto request)
        {
            if (request.Name == "") return BadRequest("Name cannot be empty!");
            if (request.Price <= 0) return BadRequest("Product price cannot be empty or <= 0");

            var newRecord = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };

            var response = await _supabase.From<Product>().Insert(newRecord);

            if (response.Models == null || !response.Models.Any())
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error inserting Data into Server. Please try again later.");
            }
            var insertedRecord = response.Models.First();            
            
            return Ok(insertedRecord.Id);
        }
    }
}
