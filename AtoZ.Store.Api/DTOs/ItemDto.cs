using System;

namespace AtoZ.Store.Api.DTOs;

public class ItemDto
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => Price * Quantity;
}
