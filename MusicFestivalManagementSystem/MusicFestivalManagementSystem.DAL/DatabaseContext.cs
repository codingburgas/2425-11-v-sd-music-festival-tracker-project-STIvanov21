using Microsoft.Data.SqlClient;

namespace MusicFestivalManagementSystem.DAL;

using Microsoft.EntityFrameworkCore;
 
public class DatabaseContext
{
    public List<Festival> Festivals { get; set; }

    public SqlConnection Connection { get; } = new SqlConnection();
    
    public DatabaseContext()
    {
        string connectionString = "Server=tcp:festivalmanagementserver.database.windows.net,1433;Initial Catalog=FestivalManagementDatabase;Persist Security Info=False;User ID=stivanov21;Password={Sob78998};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        Connection = new SqlConnection(connectionString);
        Connection.Open();
        
        Festivals = new List<Festival>();
    }

    public void ReadFestivals()
    {
        string query = "SELECT * FROM Festivals";
        SqlCommand command = new SqlCommand(query, Connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Festivals.Add(new Festival()
                {
                    ID = Convert.ToInt32(reader["FestivalID"]),
                    Name = Convert.ToString(reader["Name"]),
                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                    MainArtist = Convert.ToString(reader["MainArtist"]),
                    Location = Convert.ToString(reader["Location"]),
                    AttendeeCount = Convert.ToInt32(reader["AttendeeCount"]),
                }
            );
        }
    }
    
}