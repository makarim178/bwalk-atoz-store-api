using System.Net;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AtoZ.Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private readonly ICartService _cartService = cartService;
        //  APIs
        //  create session and cart: POST - no args, return CartDto (Main Route)
        [HttpPost]
        public async Task<IActionResult> AddCart(Guid sessionId)
        {
            try
            {
                if (sessionId == Guid.Empty) return BadRequest("Session Id is required to create a cart request!");
                var response = await _cartService.AddCart(sessionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //  Add Cart Item: Post: CartItemDto - args: CartItem ("cartItem")
        [HttpPost("cartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItem cartItem)
        {
            if (cartItem == null) return BadRequest("Cart Item cannot be null");
            if (cartItem.CartId == Guid.Empty || cartItem.ProductId == Guid.Empty) return BadRequest("Cart ID or Product ID is required!");
            if (cartItem.Quantity <= 0) return BadRequest("Item Quantity cannot be 0 or less");
            try
            {
                var response = await _cartService.AddCartItem(cartItem);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Get Cart
        [HttpGet("{sessionId:guid}")]
        public async Task<IActionResult> GetCart(Guid sessionId)
        {
            // if (cartId == Guid.Empty) return BadRequest("Cart Id is required!");
            try
            {
                var response = await _cartService.GetCart(sessionId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //  Remove Cart Item: Delete: args - cartItemId, return Boolean("cartItem")
        [HttpDelete("cartItem")]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
            if (cartItemId == Guid.Empty) return BadRequest("Cart Item Id is required");
            try
            {
                var response = await _cartService.RemoveItem(cartItemId);

                if (!response) return NotFound("Error occured while removing cartItem! Cart Item Id is not Found");

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //  Update Cart Item: PUT: Return Boolean("cartItem")
        [HttpPut("cartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDto cartItem)
        {
            if (cartItem == null) return BadRequest("Cart Item is required!");
            if (cartItem.Quantity < 0 || cartItem.Price < 0) return BadRequest("Quantity or Price cannot hold a negative value!");
            try
            {
                var response = await _cartService.UpdateCartItem(cartItem);
                if (!response) return BadRequest("Could not update Cart Item! CartItemId or ProductId is missing!");
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
