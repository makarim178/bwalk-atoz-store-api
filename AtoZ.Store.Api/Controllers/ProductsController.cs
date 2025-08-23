using System.Net;
using System.Threading.Tasks;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace AtoZ.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDto request)
        {
            if (request.Name == "") return BadRequest("Name cannot be empty!");
            if (request.Price <= 0) return BadRequest("Product price cannot be empty or <= 0");

            var newRecord = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };

            var response = await _productService.Add(newRecord);

            if (response == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error inserting Data into Server. Please try again later.");
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _productService.GetById(id);
            return Ok(response);
        }
    }
}
