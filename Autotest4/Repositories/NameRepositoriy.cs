using Autotest4.Models;
using Microsoft.Data.Sqlite;
using System.Windows.Input;

namespace Autotest4.Repositories;

public class NameRepositoriy
{
    private readonly SqliteConnection _connection;
    public NameRepositoriy()
    {
        _connection = new SqliteConnection("Data source=autotest6.db");
        _connection.Open();
        CreateName();

    }
    private void CreateName()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS  name (id INTEGER PRIMARY KEY AUTOINCREMENT,username TEXT)";
        command.ExecuteNonQuery();
    }
    public void AddName(string name)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "INSERT INTO name (username) VALUES (@username)";
        common.Parameters.AddWithValue("username", name);
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public bool Have(string username)
    {
        var command = _connection.CreateCommand();
        command.CommandText = "SELECT * FROM name WHERE username = @username";
        command.Parameters.AddWithValue("username", username);
        command.Prepare();
        Name name;
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            name = new Name()
            {
                Id = reader.GetInt32(0),
                name = reader.GetString(1)
            };
            reader.Close();
            return false;
        }
        reader.Close();

        return true;
    }
    public void DeleteName(string username)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "DELETE FROM name WHERE username = @username";
        common.Parameters.AddWithValue("username", username);
        common.Prepare();
        common.ExecuteNonQuery();
    }

}
