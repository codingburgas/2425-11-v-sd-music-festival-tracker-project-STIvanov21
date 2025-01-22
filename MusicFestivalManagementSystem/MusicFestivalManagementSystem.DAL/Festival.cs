namespace MusicFestivalManagementSystem.DAL;

public class Festival
{
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string MainArtist { get; set; }
    public string Location { get; set; }
    public int AttendeeCount { get; set; }
}