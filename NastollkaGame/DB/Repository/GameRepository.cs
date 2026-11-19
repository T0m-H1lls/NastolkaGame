using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MySqlConnector;
using NastollkaGame.Models;

namespace NastollkaGame.DB.Repository;

public class GameRepository:Base,IDisposable
{
    public GameRepository(IOptions<DataBaseConnection> options) : base(options)
    {
        OpenConnection();
    }

    public List<Game> GetGames()
    {
        List<Game> games = new();
        string sql = @"SELECT g.GameName,g2.Name,g.HoursPlayed   
                        FROM DatePlayed dp 
                        JOIN Game g on dp.GameId  = g.id 
                        join Genre g2 on g.GenreId  = g2.id ";
        try
        {
            using (var cm = new MySqlCommand(sql, connection))
            {
                using (var reader = cm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        games.Add(new Game()
                        {
                            Name = reader.GetString("GameName"),
                            Genre = reader.GetString("Name"),
                            HoursPlayed = reader.GetTimeOnly("HoursPlayed")
                        });
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return games;
    }

    public bool AddGame(Game game)
    {
        string sql = @"INSERT INTO 'Game' VALUES (0,@GameName,@GenreId))";
        try
        {
            using (var cm = new MySqlCommand(sql, connection))
            {
                cm.Parameters.AddWithValue("@GameName", game.Name);
                cm.Parameters.AddWithValue("@GenreId", game.GenreId);
                cm.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }
    
    public void Dispose()
    {
        connection.Close();
        base.Dispose();
    }
}