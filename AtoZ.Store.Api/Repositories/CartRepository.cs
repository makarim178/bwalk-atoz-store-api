using System;
using System.Security.Authentication;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using Supabase;

namespace AtoZ.Store.Api.Repositories;

public class CartRepository(Client supabase) : ICartRepository
{
    private readonly Client _supabase = supabase;

    public async Task<Cart?> GetCart(Guid? sessionId)
    {
        var cartResponse = await _supabase.From<Cart>().Where(c => c.SessionId == sessionId).Get();
        return cartResponse.Models.FirstOrDefault();
    }

    public async Task<CartItem?> AddItem(CartItem cartitem)
    {

        //Product verification is required
        var response = await _supabase.From<CartItem>().Insert(cartitem);
        return response.Models.FirstOrDefault();
    }

    public async Task<Cart> Create(Guid? sessionId)
    {
        if (sessionId == null) throw new Exception("Session Id is required!");

        var cart = await GetCart(sessionId);

        if (cart != null)
        {
            throw new Exception("Cart already exists!");
        }

        cart = new Cart { SessionId = sessionId, CreatedAt = DateTime.UtcNow };
        var response = await _supabase.From<Cart>().Insert(cart);
        return response.Models.First();
    }

    public async Task<List<CartItem>> GetItems(Guid? cartId)
    {
        if (cartId == null) throw new Exception("Cart Id cannot be null");
        var repsonse = await _supabase.From<CartItem>()
            .Where(ci => ci.CartId == cartId).Get();
        return repsonse.Models;
    }

    public async Task<Boolean> RemoveItem(Guid cartItemId)
    {
        try
        {
            await _supabase.From<CartItem>()
                .Where(c => c.Id == cartItemId)
                .Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<Boolean> UpdateItem(CartItemDto cartItem)
    {
        try
        {
            await _supabase.From<CartItem>()
                .Where(c => c.Id == cartItem.CartItemId)
                .Set(c => c.Quantity, cartItem.Quantity)
                .Update();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<CartItem?> GetCartItem(Guid? cartItemId)
    {
        if (cartItemId == null) throw new Exception("Cart Item Id is required");
        var response = await _supabase.From<CartItem>().Where(c => c.Id == cartItemId).Get();
        return response.Models.FirstOrDefault();
    }
    public async Task<CartItem?> GetCartItem(CartItem cartItem)
    {
        try
        {
            var response = await _supabase.From<CartItem>()
                .Where(ci => ci.CartId == cartItem.Id)
                .Where(ci => ci.ProductId == cartItem.ProductId).Get();
            return response.Models.FirstOrDefault();            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message, "Hello");
            return null;
        }
    }

    public async Task<Product?> GetProductById(Guid productId)
    {
        return await _supabase.From<Product>().Where(p => p.Id == productId).Single();
    }
}
