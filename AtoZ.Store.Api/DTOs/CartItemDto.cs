using System;

namespace AtoZ.Store.Api.DTOs;

public class CartItemDto: ItemDto
{
    public Guid? CartItemId { get; set; }
}
