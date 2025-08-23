using System;
using AtoZ.Store.Api.Entities;

namespace AtoZ.Store.Api.Services.Interfaces;

public interface IProductService
{
    Task<Product> Add(Product product);
    Task<List<Product>> GetAll();
    Task<Product?> GetById(Guid id);
}
