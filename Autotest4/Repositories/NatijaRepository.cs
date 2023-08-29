using Autotest4.Models;
using Microsoft.Data.Sqlite;

namespace Autotest4.Repositories;

public class NatijaRepository
{
    private readonly SqliteConnection _connection;
    public NatijaRepository()
    {
        _connection = new SqliteConnection("Data source=autotest6.db");
        _connection.Open();
        CreateNatijaTable();

    }
    private void CreateNatijaTable()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS  natija (id INTEGER PRIMARY KEY AUTOINCREMENT,userid TEXT,tisketid INTEGER,correctcount INTEGER)";
        command.ExecuteNonQuery();
    }
    public void AddNatija(Natija natija)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "INSERT INTO natija (userid,tisketid,correctcount) VALUES (@userid,@tisketid,@correctcount)";
        common.Parameters.AddWithValue("userid", natija.Userid);
        common.Parameters.AddWithValue("tisketid", natija.TisketId);
        common.Parameters.AddWithValue("correctcount", natija.correctCount);
        
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public List<Natija> GetAll()
    {
        var command = _connection.CreateCommand();
        var list = new List<Natija>();
        command.CommandText = $"SELECT * FROM natija";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Natija()
            {
                Id = reader.GetInt32(0),
                Userid = reader.GetString(1),
                TisketId = reader.GetInt32(2),
                correctCount = reader.GetInt32(3),

            });

        }
        reader.Close();
        return list;
    }
    public List<Natija> GetNatija(string userid)
    {
        var command = _connection.CreateCommand();
        var list = new List<Natija>();
        command.CommandText = $"SELECT * FROM natija WHERE userid = @userid";
        command.Parameters.AddWithValue("userid", userid);
        command.Prepare();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Natija()
            {
                Id = reader.GetInt32(0),
                Userid = reader.GetString(1),
                TisketId = reader.GetInt32(2),
                correctCount = reader.GetInt32(3),
            });
        }
        reader.Close();
        return list;
    }
}
