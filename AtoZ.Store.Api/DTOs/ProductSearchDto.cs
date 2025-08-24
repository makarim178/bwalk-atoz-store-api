using System;
using Microsoft.IdentityModel.Tokens;

namespace AtoZ.Store.Api.DTOs;

public class ProductSearchDto: PaginationDto
{
    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    

    public new bool IsValid(out List<string> errors)
    {
        if (!base.IsValid(out errors))
        {
            return false;
        }

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

        return errors.Count == 0;
    }

    
}
