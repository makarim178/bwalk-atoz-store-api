using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace AtoZ.Store.Api.Models;

[Table("order_items")]
public class OrderItem : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid? Id { get; set; }
    [Column("order_id")]
    public Guid OrderId { get; set; }
    [Column("product_id")]
    public Guid ProductId { get; set; }
    [Column("quantity")]
    public int Quantity { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
}
