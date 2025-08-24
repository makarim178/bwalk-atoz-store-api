using System;

namespace AtoZ.Store.Api.DTOs;

public class PaginationResponseDto
{
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage => TotalPages > CurrentPage;
    public bool HasPreviousPage => CurrentPage > 1;
}
