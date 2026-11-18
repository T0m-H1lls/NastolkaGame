using System;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace NastollkaGame.DB;

public abstract class Base
{
    
    protected  MySqlConnection connection;

    public Base(IOptions<DataBaseConnection> options)
    {
        connection = new MySqlConnection(options.Value.ConnectionString);
    }
    
    
    public bool OpenConnection()
    {
        try
        {
          connection.Open();
          return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool CloseConnection()
    {
        try
        {
             connection.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
       
    }
    public void Dispose()
    {
        connection.Close();
        connection.Dispose();
    }
    
}