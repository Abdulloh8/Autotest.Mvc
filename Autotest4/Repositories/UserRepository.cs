using Autotest4.Models;
using Microsoft.Data.Sqlite;
namespace Autotest4.Repositories;

public class UserRepository
{
    private readonly SqliteConnection _connection;
    public UserRepository()
    {
        _connection = new SqliteConnection("Data source=autotest6.db");
        _connection.Open();
        CreateUserTable();

    }
    private void CreateUserTable()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS  users (id TEXT UNIQUE,username TEXT,password TEXT,photo_url TEXT)";
        command.ExecuteNonQuery();
    }
    public void AddUser(User user)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "INSERT INTO users (id,username,password,photo_url) VALUES (@id,@username,@password,@photo_url)";
        common.Parameters.AddWithValue("id", user.Id);
        common.Parameters.AddWithValue("username", user.UserName);
        common.Parameters.AddWithValue("password", user.Password);
        common.Parameters.AddWithValue("photo_url", user.PhotoPath);
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public User? GetUser(string username)
    {
        var commond = _connection.CreateCommand();
        commond.CommandText = $"SELECT * FROM users WHERE username = @username";
        commond.Parameters.AddWithValue("username", username);
        commond.Prepare();
        User user;
        var reader = commond.ExecuteReader();
        while (reader.Read())
        {
            user = new User()
            {
                Id = reader.GetString(0),
                UserName = reader.GetString(1),
                Password = reader.GetString(2),
                PhotoPath = reader.GetString(3)
            };
            reader.Close();
            return user;
        }
        reader.Close();

        return null;
    }
    public void Update(User user)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "UPDATE users SET username = @username,password = @password,photo_url = @photo_url WHERE id = @id";
        common.Parameters.AddWithValue("id", user.Id);
        common.Parameters.AddWithValue("username", user.UserName);
        common.Parameters.AddWithValue("password", user.Password);
        common.Parameters.AddWithValue("photo_url", user.PhotoPath);
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public void DeleteUser(string username)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "DELETE FROM users WHERE username = @username";
        common.Parameters.AddWithValue("username", username);
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public List<User> GetUsers()
    {
        var command = _connection.CreateCommand();
        var users = new List<User>();
        command.CommandText = $"SELECT * FROM users";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            users.Add(new User()
            {
                Id = reader.GetString(0),
                UserName = reader.GetString(1),
                Password = reader.GetString(2),
                PhotoPath = reader.GetString(3),
                
            });

        }
        reader.Close();
        return users;
    }
    public User? GetUserId(string id)
    {
        var commond = _connection.CreateCommand();
        commond.CommandText = $"SELECT * FROM users WHERE id = @id";
        commond.Parameters.AddWithValue("id", id);
        commond.Prepare();
        User user;
        var reader = commond.ExecuteReader();
        while (reader.Read())
        {
            user = new User()
            {
                Id = reader.GetString(0),
                UserName = reader.GetString(1),
                Password = reader.GetString(2),
                PhotoPath = reader.GetString(3)
            };
            reader.Close();
            return user;
        }
        reader.Close();

        return null;
    }
    public void DeleteUserId(string userid)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "DELETE FROM users WHERE id = @id";
        common.Parameters.AddWithValue("id", userid);
        common.Prepare();
        common.ExecuteNonQuery();
    }
}
