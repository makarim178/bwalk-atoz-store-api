using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace AtoZ.Store.Api.Models;

[Table("cart_items")]
public class CartItem : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid? Id { get; set; }
    [Column("cart_id")]
    public Guid CartId { get; set; }
    [Column("product_id")]
    public Guid ProductId { get; set; }
    [Column("quantity")]
    public int quantity { get; set; }
}
