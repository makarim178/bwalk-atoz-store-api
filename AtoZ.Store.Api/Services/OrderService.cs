using System;
using System.Runtime.CompilerServices;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using AtoZ.Store.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AtoZ.Store.Api.Services;

public class OrderService (ICartService cartService, IOrderRepository orderRepository) : IOrderService
{
    private readonly ICartService _cartService = cartService;
    private readonly IOrderRepository _orderRepository = orderRepository;

    private async Task RevertOrder(Guid sessionId)
    {
        Boolean removeOrder = await _orderRepository.RemoveOrder(sessionId);
        Boolean changeOrderStatus = await _cartService.UpdateOrderStatus(sessionId, false);
        if (!removeOrder || !changeOrderStatus) throw new Exception("Could not revert order");
    }

    public async Task<OrderDto> CreateOrder(Guid sessionId)
    {
        try
        {

            var cart = await _cartService.GetCart(sessionId)
                ?? throw new Exception("Session Id does not have valid cart!");

            var orderItemsList = cart.Items.Select(c => new OrderItemDto
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,
                ImageUrl = c.ImageUrl,
                Price = c.Price,
                Quantity = c.Quantity
            });

            DateTime orderCreationTime = DateTime.UtcNow;
            var order = await _orderRepository.CreateOrder(new Order
            {
                SessionId = sessionId,
                CreatedAt = orderCreationTime
            }) ?? throw new Exception("Could not generate Order Id");

            if (!order.Id.HasValue) throw new Exception("Could not generate Order.");

            var orderItems = cart.Items.Select(c => new OrderItem
            {
                OrderId = order.Id.Value,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Price
            });

            var orderItemsResponse = await _orderRepository.AddOrderItems([.. orderItems])
                ?? throw new Exception("Could not generate Order Items.");
                
            var orderIdDict = orderItemsResponse
                .Where(o => o.Id != null)
                .ToDictionary(o => o.ProductId, o => o.Id);

            List<OrderItemDto> oiList = [.. orderItemsList];
            for (var i = 0; i < oiList.Count; i++)
            {
                if (orderIdDict.TryGetValue(oiList[i].ProductId, out Guid? orderItemId))
                {
                    if (orderItemId.HasValue) oiList[i].OrderItemId = orderItemId;
                }
            }

            Boolean updateOrderStatus = await _cartService.UpdateOrderStatus(sessionId, true);

            if (!updateOrderStatus)
            {
                // Revert Changes when status is false
                await RevertOrder(sessionId);
                throw new Exception("Some Error Occured: Reverted changes");
            }
            // change cart status to false

            return new OrderDto
            {
                Id = order.Id.Value,
                SessionId = sessionId,
                Items = oiList,
                CreatedAt = orderCreationTime
            };
        }
        catch
        {
            // revert changes when error occurs
            // Supabase C# does not support transaction yet
            await RevertOrder(sessionId);
            throw new Exception("Some Error Occured: Reverted changes");
        }
    }
}
