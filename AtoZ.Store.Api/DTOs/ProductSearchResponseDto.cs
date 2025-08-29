using System;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;

namespace AtoZ.Store.Api.DTOs;

public class ProductSearchResponseDto: PaginationResponseDto
{    public List<ProductDto> Products { get; set; } = [];
}
