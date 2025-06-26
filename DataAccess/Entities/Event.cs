using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;
[Table("events")]
public class Event
{
    [Column("event_id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("date")]
    public DateTime Timestamp { get; set; }

    [Column("location")]
    public string Location { get; set; }

    [Column("event_code")]
    public int Code { get; set; }

    [Column("created_by")]
    public Guid CreatedBy { get; set; }

    [Column("visibility")]
    public string? Visibility { get; set; }
    public User Owner { get; set; }
    public IList<EventResponse> Responses { get; internal set; }
    public IList<UserEvent> UserEvents { get; set; }

}
