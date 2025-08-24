using System;

namespace AtoZ.Store.Api.DTOs;

public class CartItemDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => Price * Quantity;
}
