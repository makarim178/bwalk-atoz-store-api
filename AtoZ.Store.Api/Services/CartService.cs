using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using AtoZ.Store.Api.Services.Interfaces;


namespace AtoZ.Store.Api.Services;
public class CartService(ICartRepository cartRepository) : ICartService
{
    private readonly ICartRepository _cartRepository = cartRepository;

    public async Task<CartDto?> AddCart(Guid? sessionId)
    {
        if (sessionId == null) throw new Exception("Session Id is required!");
        var cart = await _cartRepository.GetCart(sessionId);

        if (cart != null)
        {
            throw new Exception("Cart already exists with this session!");
        }
        cart = await _cartRepository.Create(sessionId);

        if (cart.Id == null) throw new Exception("Could not insert cart!");
        return await GetCart(sessionId);
    }

    public async Task<CartDto?> GetCart(Guid? sessionId)
    {
        if (sessionId == null) throw new Exception("Cart Id is required!");
        var response = await _cartRepository.GetCart(sessionId) ?? throw new Exception("Cart does not exist!");

        var cartItems = await GetCartItems(response.Id);

        return new CartDto
        {
            Id = response.Id,
            SessionId = response.SessionId,
            Items = cartItems,
            CreatedAt = response.CreatedAt
        };
    }
    public async Task<CartItemDto?> AddCartItem(CartItem cartItem)
    {
        var cartItemResponse = await _cartRepository.GetCartItem(cartItem);
        if (cartItemResponse != null) throw new Exception("Cart Item already exists");
        cartItem = new CartItem
        {
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity
        };
        var response = await _cartRepository.AddItem(cartItem);
        if (response?.Id == null) throw new Exception("Could not create cart!");

        return await GetCartItem(response);
    }


    public async Task<List<CartItemDto>> GetCartItems(Guid? cartId)
    {
        if (cartId == null) throw new Exception("Cart Id is required");

        var cartItemsResponse = await _cartRepository.GetItems(cartId);

        var cartItems = new List<CartItemDto>();

        foreach (var item in cartItemsResponse)
        {
            var cartItem = await GetCartItem(item);
            if (cartItem != null)
            {
                cartItems.Add(cartItem);
            }
        }

        return cartItems;
    }

    public async Task<CartItemDto> GetCartItem(CartItem cartItem)
    {
        var productResponse = await _cartRepository.GetProductById(cartItem.ProductId) ?? throw new Exception("Product not available!");
        if (cartItem.Id == Guid.Empty
            || cartItem.Id == null
            || productResponse.Id == Guid.Empty
            || productResponse.Id == null) throw new Exception("Cart item Id cannot be null");

        return new CartItemDto
        {
            CartItemId = (Guid)cartItem.Id,
            ProductId = (Guid)productResponse.Id,
            ProductName = productResponse.Name,
            ImageUrl = productResponse.ImageUrl,
            Price = productResponse.Price,
            Quantity = cartItem.Quantity
        };
    }

    private async Task<Boolean> IsCartItemValid(Guid? cartItemId)
    {
        if (cartItemId == Guid.Empty) return false;
        var cartItem = await _cartRepository.GetCartItem(cartItemId);
        return cartItem != null;
    }

    public async Task<Boolean> RemoveItem(Guid cartItemId)
    {
        var validCartItem = await IsCartItemValid(cartItemId);
        if (!validCartItem) return false;
        return await _cartRepository.RemoveItem(cartItemId);
    }

    public async Task<Boolean> UpdateCartItem(CartItemDto cartItem)
    {
        var validCartItem = await IsCartItemValid(cartItem.CartItemId);
        if (!validCartItem) return false;
        
        // Remove Cart Item if Quantity is 0
        if (cartItem.Quantity == 0)
        {
            var isRemove = await RemoveItem(cartItem.CartItemId);
        }
        return await _cartRepository.UpdateItem(cartItem);
    }
}
