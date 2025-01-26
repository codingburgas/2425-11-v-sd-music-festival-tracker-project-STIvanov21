namespace MusicFestivalManagementSystem;
using MusicFestivalManagementSystem.DAL;

public class MainMenu
{
    private static readonly IFestivalRepository _festivalRepository = new FestivalRepository("Server=tcp:festivalmanagementserver.database.windows.net,1433;Initial Catalog=FestivalManagementDatabase;Persist Security Info=False;User ID=stivanov21;Password=Sob78998;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    public static Menus menus = new Menus(_festivalRepository);
    
    public static void DisplayMainMenu()
    {
        string[] menuOptions = { "View events", "Add event", "Search for an event", "Exit" };
        int selectedIndex = 0;
        
        while (true)
        {
            Console.Clear();
            
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"  > {menuOptions[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"    {menuOptions[i]}");
                }
            }
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex--;
                    if (selectedIndex < 0)
                    {
                        selectedIndex = menuOptions.Length - 1;
                    }

                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex++;
                    if (selectedIndex >= menuOptions.Length)
                    {
                        selectedIndex = 0;
                    }

                    break;
                case ConsoleKey.Enter:
                    switch (selectedIndex)
                    {
                        case 0:
                            menus.ViewEvents();
                            break;
                        case 1:
                            menus.AddEvent();
                            break;
                        case 2:
                            menus.SearchEvent();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }

                    break;
            }
        }

        return;
    }
}    