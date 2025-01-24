namespace MusicFestivalManagementSystem;
using MusicFestivalManagementSystem.DAL;

public class Menus
{
    private readonly IFestivalRepository _festivalRepository;

    public Menus(IFestivalRepository festivalRepository)
    {
        _festivalRepository = festivalRepository;
    }
    
    static DatabaseContext db = new DatabaseContext();
    public void ViewEvents()
    {
        while(true)
        {
            Console.Clear();
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
                // Option menu
                Console.WriteLine("\nOptions:");
                Console.WriteLine("\t1. Go Back");
                Console.WriteLine("\t2. Edit Festival by ID");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1: // Go Back (key 1)
                        return; // Exit the ViewEvents loop

                    case ConsoleKey.D2: // Edit Festival by ID (key 2)
                        int festivalId;
                        while (true)
                        {
                            Console.WriteLine("\nEnter Festival ID to edit: ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out festivalId))
                            {
                                // Call function to edit festival by ID (replace with your implementation)
                                EditFestival(festivalId);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select 1 or 2.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No festivals found.");
            }
        }
    }

    public void EditFestival(int festivalId)
    {
        Console.WriteLine("Enter Festival ID to edit:");
        if (int.TryParse(Console.ReadLine(), out festivalId))
        {
            Festival festival = new Festival();
            festival = _festivalRepository.GetFestivalById(festivalId);
            if (festival.ID > -1)
            {
                EditFestivalMenu(festival);
            }
            else
            {
                Console.WriteLine("Festival not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private void EditFestivalMenu(Festival festival)
    {
        string[] editOptions = {
            $"Name: {festival.Name}",
            $"Start Date: {festival.StartDate:yyyy-MM-dd}",
            $"End Date: {festival.EndDate:yyyy-MM-dd}",
            $"Main Artist: {festival.MainArtist}",
            $"Location: {festival.Location}",
            $"Attendee Count: {festival.AttendeeCount}",
            "Save Changes",
            "Cancel"
        };

        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Editing Festival: {festival.Name}");

            for (int i = 0; i < editOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  > {editOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"    {editOptions[i]}");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex--;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = editOptions.Length - 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex++;
                    if (selectedIndex >= editOptions.Length)
                    {
                        selectedIndex = 0;
                    }
                    break;
                case ConsoleKey.Enter:
                    switch (selectedIndex)
                    {
                        case 0: // Name
                            Console.WriteLine("Enter new name:");
                            festival.Name = Console.ReadLine();
                            break;
                        case 1: // Start Date
                            Console.WriteLine("Enter new start date (yyyy-MM-dd):");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                            {
                                festival.StartDate = startDate;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                            }
                            break;
                        case 2: // End Date
                            Console.WriteLine("Enter new end date (yyyy-MM-dd):");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                            {
                                festival.EndDate = endDate;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format.");
                            }
                            break;
                        case 3: // Main Artist
                            Console.WriteLine("Enter new main artist:");
                            festival.MainArtist = Console.ReadLine();
                            break;
                        case 4: // Location
                            Console.WriteLine("Enter new location:");
                            festival.Location = Console.ReadLine();
                            break;
                        case 5: // Attendee Count
                            Console.WriteLine("Enter new attendee count:");
                            if (int.TryParse(Console.ReadLine(), out int attendeeCount))
                            {
                                festival.AttendeeCount = attendeeCount;
                            }
                            else
                            {
                                Console.WriteLine("Invalid attendee count.");
                            }
                            break;
                        case 6: // Save Changes
                            _festivalRepository.UpdateFestival(festival);
                            Console.WriteLine("Festival updated successfully.");
                            return;
                        case 7: // Cancel
                            Console.WriteLine("Edit canceled.");
                            return;
                    }
                    break;
            }
        }
    }

    public void AddEvent()
    {
        Console.Clear();
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
    }

    public void SearchEvent()
    {
        Console.WriteLine("\nSearching for an event...\n");
        Console.ReadKey();
    }
}