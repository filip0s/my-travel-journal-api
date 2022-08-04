namespace MyTravelJournal.Api.DTOs;

public class TripUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}