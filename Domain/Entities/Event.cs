
namespace Domain.Entities;
public class Event
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
    public string Location { get; set; }
    public int? Code { get; set; }
    public string CreatedBy { get; set; }
}
