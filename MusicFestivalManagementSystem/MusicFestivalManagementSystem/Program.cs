using MusicFestivalManagementSystem.DAL;
namespace MusicFestivalManagementSystem;

class Program
{
    static DatabaseContext db = new DatabaseContext();
    private static MainMenu mainMenu = new MainMenu();
    static void Main(string[] args)
    {
        MainMenu.DisplayMainMenu();
    }
}