using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class RunnerTests
  {
    private readonly string CONN_STRING = "Server=server_name;Database=db_name;User ID=user_id;Password=pwd;MultipleActiveResultSets=false;";

    private Runner _testRunner;

    public RunnerTests()
    {
      _testRunner = new Runner();
    }

    [Fact]
    public void CreateConnectionStringWithValidArgs()
    {
      var parsedArgs = CreateValidArgs();

      var connString = _testRunner.CreateConnectionString(parsedArgs);

      Assert.Equal(CONN_STRING, connString);
    }

    private Dictionary<string,string> CreateValidArgs()
    {
      return new Dictionary<string,string>
      {
        {"-s","server_name"},
        {"-d","db_name"},
        {"-u","user_id"},
        {"-p","pwd"}
      };
    }
  }
}