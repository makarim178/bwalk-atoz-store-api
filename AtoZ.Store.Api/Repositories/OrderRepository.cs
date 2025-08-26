using System;
using System.Collections.ObjectModel;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Supabase;

namespace AtoZ.Store.Api.Repositories;

public class OrderRepository(Client supabase) : IOrderRepository
{
    private readonly Client _supabase = supabase;
    public async Task<List<OrderItem>?> AddOrderItems(ICollection<OrderItem> items)
    {
        if (items.IsNullOrEmpty()) throw new Exception("Order Item List is empty!");
        var response = await _supabase.From<OrderItem>().Insert(items, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation });
        return response.Models;
    }

    public async Task<Order?> CreateOrder(Order order)
    {
        var response = await _supabase.From<Order>().Insert(order, new Postgrest.QueryOptions { Returning = Postgrest.QueryOptions.ReturnType.Representation} );
        Console.WriteLine(response);
        return response.Models.FirstOrDefault();
    }

    public async Task<bool> RemoveOrder(Guid sessionId)
    {
        try
        {
            await _supabase.From<Order>().Where(o => o.SessionId == sessionId).Delete();
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not revert order: {ex}");
            return false;
        }
        
    }
}
