using System;
using Microsoft.IdentityModel.Tokens;

namespace AtoZ.Store.Api.DTOs;

public class ProductSearchDto
{
    public string? Search { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 1;

    public bool IsValid(out List<string> errors)
    {
        errors = [];

        if (MinPrice.HasValue && MinPrice < 0)
        {
            errors.Add("Minimum Price cannot hold a negative value");
        }

        if (MaxPrice.HasValue && MaxPrice < 0)
        {
            errors.Add("Maximum Price cannot hold a negative value");
        }

        if (MinPrice.HasValue && MaxPrice.HasValue && MinPrice > MaxPrice)
        {
            errors.Add("Minimum price should be less than Maximum price");
        }

        if (Page <= 0)
        {
            errors.Add("Page cannot be zero or less");
        }

        if (PageSize <= 0)
        {
            errors.Add("Page size cannot be zero or less");
        }

        return errors.Count == 0;
    }

    
}
