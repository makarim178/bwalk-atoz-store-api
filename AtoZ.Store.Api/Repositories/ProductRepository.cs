using System;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using Supabase;

namespace AtoZ.Store.Api.Repositories;

public class ProductRepository(Client supabase) : IProductRepository
{
    private readonly Client _supabase = supabase;

    public async Task<List<Product>> GetAll()
    {
        var productRecords = await _supabase.From<Product>().Get();
        return productRecords.Models;
    }

    public async Task<Product?> GetById(Guid id)
    {
        var record = await _supabase.From<Product>().Where(p => p.Id == id).Get();
        return record.Models.FirstOrDefault();
    }
    public async Task<Product> Add(Product product)
    {
        var response = await _supabase.From<Product>().Insert(product);
        return response.Models.First();
    }
}
