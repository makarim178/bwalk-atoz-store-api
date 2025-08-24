using System;

namespace AtoZ.Store.Api.DTOs;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public decimal Total => Items.Sum(i => i.LineTotal);
    public DateTime CreatedAt { get; set; }
}
