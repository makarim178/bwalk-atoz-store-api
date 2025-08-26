using System;
using AtoZ.Store.Api.DTOs;

namespace AtoZ.Store.Api.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrder(Guid sessionId);
}
