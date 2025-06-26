using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

[Table("event_responses")]
public class EventResponse
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("event_code")]
    public int EventCode { get; set; }

    [Column("response")]
    public string Response { get; set; }

    [Column("email")]
    public string Email { get; set; }
    public Event Event { get; set; }
}
