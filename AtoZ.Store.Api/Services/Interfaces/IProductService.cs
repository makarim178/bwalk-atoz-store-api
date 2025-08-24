using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Entities;

namespace AtoZ.Store.Api.Services.Interfaces;

public interface IProductService
{
    Task<Product> Add(Product product);
    Task<ProductListResponseDto> GetAll(PaginationDto paginationSearchCriteria);
    Task<Product?> GetById(Guid id);
    Task<ProductSearchResponseDto> Search(ProductSearchDto searchCriteria);
}
