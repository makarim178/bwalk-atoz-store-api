using System;

namespace AtoZ.Store.Api.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
    public decimal Total => Items.Sum(i => i.LineTotal);
    public DateTime CreatedAt { get; set; }
}
