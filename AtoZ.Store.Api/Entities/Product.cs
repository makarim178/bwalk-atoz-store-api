using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace AtoZ.Store.Api.Entities;

[Table("product")]
public class Product: BaseModel
{
    [PrimaryKey("id", false)]
    public string Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("price")]
    public double Price { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

}
