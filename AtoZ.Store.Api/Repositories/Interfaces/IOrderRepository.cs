using System;
using System.Collections.ObjectModel;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Models;

namespace AtoZ.Store.Api.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order?> CreateOrder(Order order);
    Task<List<OrderItem>?> AddOrderItems(ICollection<OrderItem> items);

    Task<Boolean> RemoveOrder(Guid sessionId);
}
