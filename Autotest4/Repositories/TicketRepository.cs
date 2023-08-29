using Autotest4.Extions;
using Autotest4.Models;
using Microsoft.Data.Sqlite;

namespace Autotest4.Repositories;

public class TicketRepository
{
    private readonly SqliteConnection _connection;
    public TicketRepository()
    {
        _connection = new SqliteConnection("Data source=autotest6.db");
        _connection.Open();
        CreateTicketTable();

    }
    private void CreateTicketTable()
    {
        var command = _connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS  ticket (id TEXT UNIQUE,ticket1 INTEGER,ticket2 INTEGER,ticket3 INTEGER,ticket4 INTEGER,ticket5 INTEGER,ticket6 INTEGER,ticket7 INTEGER,ticket8 INTEGER,ticket9 INTEGER,ticket10 INTEGER,ticket11 INTEGER,ticket12 INTEGER,ticket13 INTEGER,ticket14 INTEGER,ticket15 INTEGER,ticket16 INTEGER,ticket17 INTEGER,ticket18 INTEGER,ticket19 INTEGER,ticket20 INTEGER,ticket21 INTEGER,ticket22 INTEGER,ticket23 INTEGER,ticket24 INTEGER,ticket25 INTEGER,ticket26 INTEGER,ticket27 INTEGER,ticket28 INTEGER,ticket29 INTEGER,ticket30 INTEGER,ticket31 INTEGER,ticket32 INTEGER,ticket33 INTEGER,ticket34 INTEGER,ticket35 INTEGER,ticket36 INTEGER,ticket37 INTEGER,ticket38 INTEGER,ticket39 INTEGER,ticket40 INTEGER,ticket41 INTEGER,ticket42 INTEGER,ticket43 INTEGER,ticket44 INTEGER,ticket45 INTEGER,ticket46 INTEGER,ticket47 INTEGER,ticket48 INTEGER,ticket49 INTEGER,ticket50 INTEGER,ticket51 INTEGER,ticket52 INTEGER,ticket53 INTEGER,ticket54 INTEGER,ticket55 INTEGER,ticket56 INTEGER,ticket57 INTEGER,ticket58 INTEGER,ticket59 INTEGER,ticket60 INTEGER,ticket61 INTEGER,ticket62 INTEGER,ticket63 INTEGER,ticket64 INTEGER,ticket65 INTEGER,ticket66 INTEGER,ticket67 INTEGER,ticket68 INTEGER,ticket69 INTEGER,ticket70 INTEGER)";
        command.ExecuteNonQuery();
    }
    public void AddTicket(string userid)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "INSERT INTO ticket (id,ticket1,ticket2,ticket3,ticket4,ticket5,ticket6,ticket7,ticket8,ticket9,ticket10,ticket11,ticket12,ticket13,ticket14,ticket15,ticket16,ticket17,ticket18,ticket19,ticket20,ticket21,ticket22,ticket23,ticket24,ticket25,ticket26,ticket27,ticket28,ticket29,ticket30,ticket31,ticket32,ticket33,ticket34,ticket35,ticket36,ticket37,ticket38,ticket39,ticket40,ticket41,ticket42,ticket43,ticket44,ticket45,ticket46,ticket47,ticket48,ticket49,ticket50,ticket51,ticket52,ticket53,ticket54,ticket55,ticket56,ticket57,ticket58,ticket59,ticket60,ticket61,ticket62,ticket63,ticket64,ticket65,ticket66,ticket67,ticket68,ticket69,ticket70) VALUES (@id,@ticket1,@ticket2,@ticket3,@ticket4,@ticket5,@ticket6,@ticket7,@ticket8,@ticket9,@ticket10,@ticket11,@ticket12,@ticket13,@ticket14,@ticket15,@ticket16,@ticket17,@ticket18,@ticket19,@ticket20,@ticket21,@ticket22,@ticket23,@ticket24,@ticket25,@ticket26,@ticket27,@ticket28,@ticket29,@ticket30,@ticket31,@ticket32,@ticket33,@ticket34,@ticket35,@ticket36,@ticket37,@ticket38,@ticket39,@ticket40,@ticket41,@ticket42,@ticket43,@ticket44,@ticket45,@ticket46,@ticket47,@ticket48,@ticket49,@ticket50,@ticket51,@ticket52,@ticket53,@ticket54,@ticket55,@ticket56,@ticket57,@ticket58,@ticket59,@ticket60,@ticket61,@ticket62,@ticket63,@ticket64,@ticket65,@ticket66,@ticket67,@ticket68,@ticket69,@ticket70)";
        common.Parameters.AddWithValue("id", userid);
        for (int i = 1; i <= 70; i++)
        {
            common.Parameters.AddWithValue($"ticket{i}", 20);
        }
        
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public Ticket? GetTicket(string userid)
    {
        var commond = _connection.CreateCommand();
        commond.CommandText = $"SELECT * FROM ticket WHERE id = @id";
        commond.Parameters.AddWithValue("id", userid);
        commond.Prepare();
        Ticket ticket = new Ticket();
        var reader = commond.ExecuteReader();
        while (reader.Read())
        {
            ticket = reader.GetTicket();
            
            reader.Close();
            return ticket;
        }
        reader.Close();

        return null;
    }
    public void Update(string name, int value, string userid)
    {
        var common = _connection.CreateCommand();
        common.CommandText = $"UPDATE  ticket SET {name} = @{name}  WHERE id = @userid";
        common.Parameters.AddWithValue($"{name}", value);
        common.Parameters.AddWithValue("userid", userid);
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public void Delete(string userid)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "DELETE FROM ticket WHERE id = @userid";
        common.Parameters.AddWithValue("userid", userid);
        common.Prepare();
        common.ExecuteNonQuery();
    }
    public void Obv(string id)
    {
        var common = _connection.CreateCommand();
        common.CommandText = "UPDATE ticket SET ticket1 = @ticket1,ticket2 = @ticket2,ticket3 = @ticket3,ticket4 = @ticket4,ticket5 = @ticket5,ticket6 = @ticket6,ticket7 = @ticket7,ticket8 = @ticket8,ticket9 = @ticket9,ticket10 = @ticket10,ticket11 = @ticket11,ticket12 = @ticket12,ticket13 = @ticket13,ticket14 = @ticket14,ticket15 = @ticket15,ticket16 = @ticket16,ticket17 = @ticket17,ticket18 = @ticket18,ticket19 = @ticket19,ticket20 = @ticket20,ticket21 = @ticket21,ticket22 = @ticket22,ticket23 = @ticket23,ticket24 = @ticket24,ticket25 = @ticket25,ticket26 = @ticket26,ticket27 = @ticket27,ticket28 = @ticket28,ticket29 = @ticket29,ticket30 = @ticket30,ticket31 = @ticket31,ticket32 = @ticket32,ticket33 = @ticket33,ticket34 = @ticket34,ticket35 = @ticket35,ticket36 = @ticket36,ticket37 = @ticket37,ticket38 = @ticket38,ticket39 = @ticket39,ticket40 = @ticket40,ticket41 = @ticket41,ticket42 = @ticket42,ticket43 = @ticket43,ticket44 = @ticket44,ticket45 = @ticket45,ticket46 = @ticket46,ticket47 = @ticket47,ticket48 = @ticket48,ticket49 = @ticket49,ticket50 = @ticket50,ticket51 = @ticket51,ticket52 = @ticket52,ticket53 = @ticket53,ticket54 = @ticket54,ticket55 = @ticket55,ticket56 = @ticket56,ticket57 = @ticket57,ticket58 = @ticket58,ticket59 = @ticket59,ticket60 = @ticket60,ticket61 = @ticket61,ticket62 = @ticket62,ticket63 = @ticket63,ticket64 = @ticket64,ticket65 = @ticket65,ticket66 = @ticket66,ticket67 = @ticket67,ticket68 = @ticket68,ticket69 = @ticket69,ticket70 = @ticket70 WHERE id = @id";
        common.Parameters.AddWithValue("id", id);
        for (int i = 1; i <= 70; i++)
        {
            common.Parameters.AddWithValue($"ticket{i}", 20);
        }
        common.Prepare();
        common.ExecuteNonQuery();
    }
}
