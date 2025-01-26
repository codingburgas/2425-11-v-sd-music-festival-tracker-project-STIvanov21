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
                string[] options = { "Go Back", "Edit Festival by ID" };
                int selectedIndex = 0;

                while (true)
                {
                    Console.Clear();
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
                    Console.WriteLine("Options:");

                    for (int i = 0; i < options.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"  > {options[i]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"    {options[i]}");
                        }
                    }

                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                            break;

                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                            break;

                        case ConsoleKey.Enter:
                            if (selectedIndex == 0)
                            {
                                return;
                            }
                            else if (selectedIndex == 1)
                            {
                                int festivalId;
                                Console.WriteLine("\nEnter Festival ID to edit: ");
                                string input = Console.ReadLine();

                                if (int.TryParse(input, out festivalId))
                                {
                                    EditFestival(festivalId);
                                }
                                return;
                            }
                            break;
                    }
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
                        case 0:
                            Console.WriteLine("Enter new name:");
                            festival.Name = Console.ReadLine();
                            break;
                        case 1:
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
                        case 2:
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
                        case 3:
                            Console.WriteLine("Enter new main artist:");
                            festival.MainArtist = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Enter new location:");
                            festival.Location = Console.ReadLine();
                            break;
                        case 5:
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
                        case 6:
                            _festivalRepository.UpdateFestival(festival);
                            Console.WriteLine("Festival updated successfully.");
                            return;
                        case 7:
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
        Console.WriteLine("Enter Festival ID to search:");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int festivalId))
        {
            Festival festival = _festivalRepository.GetFestivalById(festivalId);

            if (festival.ID != -1)
            {
                Console.WriteLine($"Festival found: {festival.Name}");
                EditFestivalMenu(festival);
            }
            else
            {
                Console.WriteLine("Festival not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the Festival ID.");
        }
    }
}