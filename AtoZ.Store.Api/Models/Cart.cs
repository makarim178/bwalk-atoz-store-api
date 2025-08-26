using System;
using Postgrest.Attributes;
using Postgrest.Models;

namespace AtoZ.Store.Api.Models;

[Table("carts")]
public class Cart : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid? Id { get; set; }
    [Column("session_id")]
    public Guid? SessionId { get; set; }
    [Column("is_ordered")]
    public Boolean IsOrdered { get; set; } = false;
    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
}
