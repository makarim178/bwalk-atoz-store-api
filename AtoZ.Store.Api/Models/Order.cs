using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace AtoZ.Store.Api.Models;

[Table("orders")]
public class Order : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid? Id { get; set; }
    [Column("session_id")]
    public Guid SessionId { get; set; }
    [Column("total")]
    public decimal Total { get; set; }
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
}
