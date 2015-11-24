using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace XSqlClient
{
  public class Program
  {
    private static readonly Dictionary<string,string> _allowedArguments = new Dictionary<string,string> 
    {
      {"-s", "serverName"},
      {"-d", "databaseName"},
      {"-u", "userName"},
      {"-p", "password"}
    };

    public static void Main(string[] args)
    {
      var _argParser = new ArgumentParser(_allowedArguments);

      var _parsedArgs = _argParser.ParseArguments(args);

      if(!_argParser.ValidateArgs(_parsedArgs))
      {
        Console.WriteLine(Usage());
        //System.Environment.Exit(1);
      }
      else
      {
        Console.WriteLine("So far, so good.");
      }
    }

    public static string Usage()
    {
      return "Fill me in.";
    }
  }
}

/*
    public static void Main(string[] args)
    {
      var connectionString = "Server=.;Database=<DBName>;User ID=<user>;Password=<pwd>;MultipleActiveResultSets=false";
      var sql = "SELECT * FROM <TableName>";

      using (var connection = new SqlConnection(connectionString))
      {
        connection.Open();
        var cmd = new SqlCommand(sql, connection);

        var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
          Console.WriteLine("{0}|{1}|{2}", reader[0],reader[1],reader[2]);
        }
      }
    }
 */