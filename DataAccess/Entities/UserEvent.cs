using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("user_event")]
public class UserEvent
{
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("event_id")]
    public Guid EventId { get; set; }

    public User User { get; set; }
    public Event Event { get; set; }
}
