using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;

namespace AtoZ.Store.Api.Repositories.Interfaces;

public interface ICartRepository
{
    Task<Cart> Create(Guid? sessionId);
    Task<Cart?> GetCart(Guid? sessionId);
    Task<CartItem?> GetCartItem(Guid? cartItemId);
    Task<CartItem?> GetCartItem(CartItem cartItem);
    Task<List<CartItem>> GetItems(Guid? cartId);
    Task<CartItem?> AddItem(CartItem cartItem);
    Task<Boolean> UpdateItem(CartItemDto cartItem);
    Task<Boolean> RemoveItem(Guid cartItemId);
    Task<Product?> GetProductById(Guid productId);
    Task<Boolean> UpdateOrderStatus(Guid sessionId, Boolean value);
}
