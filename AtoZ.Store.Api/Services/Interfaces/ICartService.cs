using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Models;

namespace AtoZ.Store.Api.Services.Interfaces;

public interface ICartService
{
    Task<CartDto?> AddCart(Guid? sessionId);
    Task<CartDto?> GetCart(Guid? sessionId);
    Task<CartItemDto?> AddCartItem(CartItem cartItem);
    Task<List<CartItemDto>> GetCartItems(Guid? cartId);

    Task<CartItemDto> GetCartItem(CartItem cartItem);
    Task<Boolean> UpdateCartItem(CartItemDto cartItem);
    Task<Boolean> RemoveItem(Guid cartItemId);
}
