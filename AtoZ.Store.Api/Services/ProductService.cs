using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Repositories.Interfaces;
using AtoZ.Store.Api.Services.Interfaces;

namespace AtoZ.Store.Api.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Product> Add(Product product)
    {
        return await _productRepository.Add(product);
    }
    public async Task<List<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    public async Task<ProductSearchResponseDto> Search(ProductSearchDto searchCriteria)
    {
        var (products, totalCount) = await _productRepository.Search(searchCriteria);
        var TotalPages = (int)Math.Ceiling((double)totalCount / searchCriteria.PageSize);
        return new ProductSearchResponseDto
        {
            Products = products,
            TotalCount = totalCount,
            CurrentPage = searchCriteria.Page,
            PageSize = searchCriteria.PageSize,
            TotalPages = TotalPages
        };
    }
}
