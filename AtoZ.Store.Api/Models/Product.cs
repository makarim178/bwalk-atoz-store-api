using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace AtoZ.Store.Api.Entities;

[Table("products")]
public class Product : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid? Id { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("price")]
    public decimal Price { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    [Column("created_at", ignoreOnInsert:true)]
    public DateTime? CreatedAt { get; set; }
}
