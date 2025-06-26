namespace Domain.Entities;
public class EventResponse
{
    private string response;

    private static readonly HashSet<string> ValidResponses = new HashSet<string>
    {
        "going",
        "interested",
        "declined"
    };

    public Guid? Id {  get; set; }
    public string Name { get; set; }
    public int EventCode { get; set; }
    public string Response
    {
        get => response;
        set
        {
            if (value == null || !ValidResponses.Contains(value.ToLowerInvariant().Trim()))
            {
                throw new ArgumentException($"Invalid response: {value}. Allowed values are: going, interested, declined.");
            }

            response = value.ToLowerInvariant().Trim();
        }
    }
    public string Email { get; set; }
}
