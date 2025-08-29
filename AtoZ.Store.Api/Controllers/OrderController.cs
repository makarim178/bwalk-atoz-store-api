using System.Net;
using AtoZ.Store.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtoZ.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private IOrderService _orderService = orderService;
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Guid sessionId)
        {
            try
            {
                if (sessionId == Guid.Empty) return BadRequest("Session Id is required to create an order");
                var response = await _orderService.CreateOrder(sessionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                if (orderId == Guid.Empty) return BadRequest("Order Id is required to get an order");
                var response = await _orderService.GetOrderById(orderId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
