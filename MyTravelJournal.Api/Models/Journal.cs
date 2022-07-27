namespace MyTravelJournal.Api.Models;

public class Journal
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}