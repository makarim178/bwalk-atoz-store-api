using System;

namespace AtoZ.Store.Api.DTOs;

public class OrderItemDto: ItemDto
{
    public Guid? OrderItemId { get; set; }
}
