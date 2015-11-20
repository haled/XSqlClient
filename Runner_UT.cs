using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class RunnerTests
  {
    private string[] _testArgs;
    private Runner _testRunner;

    public RunnerTests()
    {
      _testArgs = CreateTestArgs();
      _testRunner = new Runner();
    }

    [Fact]
    public void ParseArgumentsReturnsNullWhenGivenNull()
    {
      var parsedArgs = _testRunner.ParseArguments(null);

      Assert.Null(parsedArgs);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(99)]
    public void ParseArgumentsThrowsAnExceptionIfOddNumberOfArgs(int argLength)
    {
      string[] oddArgs = new string[argLength];
      for(int i = 0; i < argLength; i++)
      {
        oddArgs[i] = "bogus arg";
      }

      var exception = Record.Exception(() => _testRunner.ParseArguments(oddArgs));
      Assert.NotNull(exception);
      Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void ParseArgumentsReturnsDictionaryOfArgsKeyedByFlag()
    {
      string[] args = new string[2];
      args[0] = "-x";
      args[1] = "data";

      var parsedArgs = _testRunner.ParseArguments(args);

      Assert.NotNull(parsedArgs);
      Assert.Equal(1, parsedArgs.Keys.Count);
      Assert.Equal("data", parsedArgs["-x"]);
    }

    [Fact]
    public void ParseArgumentsReturnsDictionaryOfArgsKeyedByFlagBeyondASingleArg()
    {
      string[] args = CreateTestArgs();

      var parsedArgs = _testRunner.ParseArguments(args);

      Assert.NotNull(parsedArgs);
      Assert.Equal(4, parsedArgs.Keys.Count);
      Assert.Equal("server_name", parsedArgs["-s"]);
      Assert.Equal("user_id", parsedArgs["-u"]);
      Assert.Equal("pwd", parsedArgs["-p"]);
    }

    [Fact]
    public void CreateConnectionStringWithValidArgs()
    {
      var parsedArgs = CreateValidArgs();

      var connString = _testRunner.CreateConnectionString(parsedArgs);

      Assert.Equal(CONN_STRING, connString);
    }

    private readonly string CONN_STRING = "Server=server_name;Database=db_name;User ID=user_id;Password=pwd;MultipleActiveResultSets=false;";

    private string[] CreateTestArgs()
    {
      string[] _args =  new string[8];
      _args[0] = "-s";
      _args[1] = "server_name";
      _args[2] = "-d";
      _args[3] = "db_name";
      _args[4] = "-u";
      _args[5] = "user_id";
      _args[6] = "-p";
      _args[7] = "pwd";

      return _args;
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