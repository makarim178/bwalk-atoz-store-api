using System;
using AtoZ.Store.Api.Entities;

namespace AtoZ.Store.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product> Add(Product product);
    Task<List<Product>> GetAll();
    Task<Product?> GetById(Guid id);
}
