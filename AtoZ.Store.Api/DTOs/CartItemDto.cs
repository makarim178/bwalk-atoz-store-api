using System;

namespace AtoZ.Store.Api.DTOs;

public class CartItemDto
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => Price * Quantity;
}
