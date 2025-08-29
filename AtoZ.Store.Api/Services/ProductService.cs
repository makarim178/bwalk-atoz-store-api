using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using AtoZ.Store.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AtoZ.Store.Api.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> Add(Product product)
    {
        return await _productRepository.Add(product);
    }

    private List<ProductDto> ConvertProductTypeList(List<Product> products)
    {
        var productRes = products.Where(p => p.Id != null).Select(p => new ProductDto
        {
            ProductId = p.Id.Value,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        });

        return [.. productRes];
    }

    public async Task<ProductListResponseDto> GetAll(PaginationDto paginationSearchCriteria)
    {
        var (products, totalCount) = await _productRepository.GetAll(paginationSearchCriteria);
        if (products.IsNullOrEmpty()) throw new Exception("Unable to retrieve product list");
        var productRes = ConvertProductTypeList(products);
        var TotalPages = (int)Math.Ceiling((double)totalCount / paginationSearchCriteria.PageSize);
        return new ProductListResponseDto
        {
            Products = productRes,
            TotalCount = totalCount,
            CurrentPage = paginationSearchCriteria.Page,
            PageSize = paginationSearchCriteria.PageSize,
            TotalPages = TotalPages
        };
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    public async Task<ProductSearchResponseDto> Search(ProductSearchDto searchCriteria)
    {
        var (products, totalCount) = await _productRepository.Search(searchCriteria);

        var productRes = ConvertProductTypeList(products);
        var TotalPages = (int)Math.Ceiling((double)totalCount / searchCriteria.PageSize);
        return new ProductSearchResponseDto
        {
            Products = productRes,
            TotalCount = totalCount,
            CurrentPage = searchCriteria.Page,
            PageSize = searchCriteria.PageSize,
            TotalPages = TotalPages
        };
    }
}
