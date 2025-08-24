using System.Net;
using System.Threading.Tasks;
using AtoZ.Store.Api.DTOs;
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
            try
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
                return Ok(response);                
            }
            catch (Exception error)
            {
                    return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
                
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDto paginationSearchCriteria)
        {
            try
            {
                var response = await _productService.GetAll(paginationSearchCriteria);
                return Ok(response);
            }
            catch (Exception error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _productService.GetById(id);
                return Ok(response);                
            }
            catch (Exception error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] ProductSearchDto searchCriteria)
        {
            if (!searchCriteria.IsValid(out var errors))
            {
                return BadRequest(new { Errors = errors });
            }

            try
            {
                var response = await _productService.Search(searchCriteria);
                return Ok(response);
            }
            catch (Exception error)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, error.Message);
            }
        }
    }
}
