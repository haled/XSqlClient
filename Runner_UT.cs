using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class RunnerTests
  {
    private readonly string CONN_STRING = "Server=server_name;Database=db_name;User ID=user_id;Password=pwd;MultipleActiveResultSets=false;";
    private readonly string USAGE_STRING = "Usage:\n\nXSqlClient <args>\n\n    -s <server_name>\n    -d <database_name>\n    -u <user_name>\n    -p <password>\n\n";

    private IRunner _testRunner;
    private IArgumentParser _mockArgParser;

    public RunnerTests()
    {
      _mockArgParser = new MockArgumentParser();
      _testRunner = new Runner();
    }

    [Fact]
    public void CreateConnectionStringWithValidArgs()
    {
      var parsedArgs = CreateValidArgs();

      var connString = _testRunner.CreateConnectionString(parsedArgs);

      Assert.Equal(CONN_STRING, connString);
    }

    [Fact]
    public void UsageStatementIsCorrect()
    {
      string usageString = _testRunner.Usage();

      Assert.Equal(USAGE_STRING, usageString);
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