using System;
using AtoZ.Store.Api.Entities;

namespace AtoZ.Store.Api.DTOs;

public class ProductSearchResponseDto: PaginationResponseDto
{    public List<Product> Products { get; set; } = [];
}
