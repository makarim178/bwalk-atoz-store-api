using System;
using AtoZ.Store.Api.Entities;

namespace AtoZ.Store.Api.DTOs;

public class ProductSearchResponseDto
{
    public List<Product> Products { get; set; } = [];
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage => TotalPages > CurrentPage;
    public bool HasPreviousPage => CurrentPage > 1;
}
