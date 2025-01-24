using MusicFestivalManagementSystem.DAL;
namespace MusicFestivalManagementSystem;

class Program
{
    static DatabaseContext db = new DatabaseContext();
    static void Main(string[] args)
    {
        Festival newFestival = new Festival();
        
                Console.WriteLine("Enter Festival Name:");
                newFestival.Name = Console.ReadLine();
        
                Console.WriteLine("Enter Start Date (yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    newFestival.StartDate = startDate;
                }
                else
                {
                    Console.WriteLine("Invalid Start Date format.");
                    return;
                }
        
                Console.WriteLine("Enter End Date (yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    newFestival.EndDate = endDate;
                }
                else
                {
                    Console.WriteLine("Invalid End Date format.");
                    return;
                }
        
                Console.WriteLine("Enter Main Artist:");
                newFestival.MainArtist = Console.ReadLine();
        
                Console.WriteLine("Enter Location:");
                newFestival.Location = Console.ReadLine();
        
                Console.WriteLine("Enter Attendee Count:");
                if (int.TryParse(Console.ReadLine(), out int attendeeCount))
                {
                    newFestival.AttendeeCount = attendeeCount;
                }
                else
                {
                    Console.WriteLine("Invalid Attendee Count format.");
                    return;
                }

                db.SaveFestival(newFestival);
                Console.WriteLine("Festival information entered successfully!");
                
                db.ReadFestivals(); 

                if (db.Festivals.Count > 0)
                {
                    Console.WriteLine("List of Festivals:");
                    foreach (Festival festival in db.Festivals)
                    {
                        Console.WriteLine($"\t- Name: {festival.Name}");
                        Console.WriteLine($"\t\tID: {festival.ID}");
                        Console.WriteLine($"\t\tStart Date: {festival.StartDate.ToString("yyyy-MM-dd")}");
                        Console.WriteLine($"\t\tEnd Date: {festival.EndDate.ToString("yyyy-MM-dd")}");
                        Console.WriteLine($"\t\tMain Artist: {festival.MainArtist}");
                        Console.WriteLine($"\t\tLocation: {festival.Location}");
                        Console.WriteLine($"\t\tAttendee Count: {festival.AttendeeCount}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No festivals found.");
                }
    }
}