using System;
using AtoZ.Store.Api.DTOs;
using AtoZ.Store.Api.Entities;
using AtoZ.Store.Api.Models;
using AtoZ.Store.Api.Repositories.Interfaces;
using Supabase;

namespace AtoZ.Store.Api.Repositories;

public class ProductRepository(Client supabase) : IProductRepository
{
    private readonly Client _supabase = supabase;

    public async Task<(List<Product> products, int totalCount)> GetAll(PaginationDto paginationSearchCriteria)
    {
        var productRecords = await _supabase.From<Product>().Get();
        int offset = (paginationSearchCriteria.Page - 1) * paginationSearchCriteria.PageSize;
        var paginatedProducts = productRecords.Models.Skip(offset).Take(paginationSearchCriteria.PageSize).ToList();
        return (paginatedProducts, productRecords.Models.Count);
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

    public async Task<(List<Product> products, int totalCount)> Search(ProductSearchDto searchCriteria)
    {
        var searchQuery = _supabase.From<Product>();
        if (!string.IsNullOrWhiteSpace(searchCriteria.Search))
        {
            var searchTerm = searchCriteria.Search.Trim().ToLower();
            searchQuery = (Supabase.Interfaces.ISupabaseTable<Product, Supabase.Realtime.RealtimeChannel>)
                searchQuery.Filter(p => p.Name, Postgrest.Constants.Operator.ILike, $"%{searchTerm}%");
        }

        if (searchCriteria.MinPrice.HasValue)
        {
            searchQuery = (Supabase.Interfaces.ISupabaseTable<Product, Supabase.Realtime.RealtimeChannel>)
                searchQuery.Where(p => p.Price >= searchCriteria.MinPrice.Value);
        }

        if (searchCriteria.MaxPrice.HasValue)
        {
            searchQuery = (Supabase.Interfaces.ISupabaseTable<Product, Supabase.Realtime.RealtimeChannel>)
                searchQuery.Where(p => p.Price <= searchCriteria.MaxPrice.Value);            
        }


        var result = await searchQuery.Get();

        int offset = (searchCriteria.Page - 1) * searchCriteria.PageSize;
        var paginatedProducts = result.Models.Skip(offset).Take(searchCriteria.PageSize).ToList();

        return (paginatedProducts, result.Models.Count);

    }
}
