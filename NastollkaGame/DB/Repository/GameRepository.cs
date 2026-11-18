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
        string sql = @"SELECT g.GameName,g2.Name,dp2.HoursPlayed  
                        FROM DatePlayed dp2 
                        JOIN Game g  on dp2.GameId  = g.id
                        JOIN Genre g2 on g.GenreId  = g2.id ";
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
    public void Dispose()
    {
        connection.Close();
        base.Dispose();
    }
}