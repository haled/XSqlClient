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
      _testRunner = new Runner(_mockArgParser);
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

    [Fact]
    public void RunReturnsZeroExitCodeWithValidArguments()
    {
      string[] args = new string[4];
      Dictionary<string,string> fakeArgs = new Dictionary<string,string>();
      bool fakeValidationResult = true;
      ((MockArgumentParser)_mockArgParser).MockParsedArgs = fakeArgs;
      ((MockArgumentParser)_mockArgParser).ExpectedValidationResult = fakeValidationResult;

      var exitCode = _testRunner.Run(args);

      Assert.True(((MockArgumentParser)_mockArgParser).CalledParseArguments);
      Assert.True(((MockArgumentParser)_mockArgParser).CalledValidateArgs);
      Assert.Equal(0, exitCode);
    }

    [Fact]
    public void RunReturnsOneExitCodeWithInvalidArguments()
    {
      string[] args = new string[4];
      Dictionary<string,string> fakeArgs = new Dictionary<string,string>();
      bool fakeValidationResult = false;
      ((MockArgumentParser)_mockArgParser).MockParsedArgs = fakeArgs;
      ((MockArgumentParser)_mockArgParser).ExpectedValidationResult = fakeValidationResult;

      var exitCode = _testRunner.Run(args);

      Assert.True(((MockArgumentParser)_mockArgParser).CalledParseArguments);
      Assert.True(((MockArgumentParser)_mockArgParser).CalledValidateArgs);
      Assert.Equal(1, exitCode);
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

  public class MockArgumentParser : IArgumentParser
  {
    public bool ExpectedValidationResult { get; set; }
    public Dictionary<string,string> MockParsedArgs { get; set; }
    public bool CalledParseArguments { get; set; }
    public bool CalledValidateArgs { get; set; }

    public MockArgumentParser()
    {
      CalledParseArguments = false;
      CalledValidateArgs = false;
    }

    public Dictionary<string,string> ParseArguments(string[] args)
    {
      CalledParseArguments = true;
      return MockParsedArgs;
    }

    public bool ValidateArgs(Dictionary<string,string> args)
    {
      CalledValidateArgs = true;
      return ExpectedValidationResult;
    }
  }
}