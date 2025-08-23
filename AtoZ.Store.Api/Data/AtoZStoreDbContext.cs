using System;
using AtoZ.Store.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AtoZ.Store.Api.Data;

public class AtoZStoreDbContext(DbContextOptions<AtoZStoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Prodcuts { get; set; }
}
