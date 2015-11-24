using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace XSqlClient
{
  public class Program
  {
    private IArgumentParser _argumentParser;
    private IRunner _runner;

    private static readonly Dictionary<string,string> _allowedArguments = new Dictionary<string,string> 
    {
      {"-s", "serverName"},
      {"-d", "databaseName"},
      {"-u", "userName"},
      {"-p", "password"}
    };

    public Program(IArgumentParser argParser, IRunner runner)
    {
      _argumentParser = argParser;
      _runner = runner;
    }

    public static int Main(string[] args)
    {
      var _argParser = new ArgumentParser(_allowedArguments);
      var _progRunner = new Runner();
      var _prog = new Program(_argParser, _progRunner);
      
      return _prog.Run(args);
    }

    public int Run(string[] args)
    {
      int _exitCode = 0;

      var _parsedArgs = _argumentParser.ParseArguments(args);

      if(!_argumentParser.ValidateArgs(_parsedArgs))
      {
        Console.WriteLine(_runner.Usage());
        _exitCode = 1;
      }
      else
      {
        var connString = _runner.CreateConnectionString(_parsedArgs);

        
        _exitCode = 0;
      }

      return _exitCode;
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