using System;
namespace AtoZ.Store.Api.Models;

public class ProductDto 
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; }
}
