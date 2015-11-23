using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class ArgumentParserTests
  {
    private Dictionary<string,string> _desiredArgs;

    private string[] _testArgs;
    private ArgumentParser _testArgParser;

    public ArgumentParserTests()
    {
      _desiredArgs = new Dictionary<string,string> 
                                                                                   {
        {"-s", "server"},
        {"-d", "database"},
        {"-u", "user"},
        {"-p", "password"},
                                                                                   };
      _testArgs = CreateTestArgs();
      _testArgParser = new ArgumentParser(_desiredArgs);
    }

    [Fact]
    public void ParseArgumentsReturnsNullWhenGivenNull()
    {
      var parsedArgs = _testArgParser.ParseArguments(null);

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

      var exception = Record.Exception(() => _testArgParser.ParseArguments(oddArgs));
      Assert.NotNull(exception);
      Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void ParseArgumentsReturnsDictionaryOfArgsKeyedByFlag()
    {
      string[] args = new string[2];
      args[0] = "-x";
      args[1] = "data";

      var parsedArgs = _testArgParser.ParseArguments(args);

      Assert.NotNull(parsedArgs);
      Assert.Equal(1, parsedArgs.Keys.Count);
      Assert.Equal("data", parsedArgs["-x"]);
    }

    [Fact]
    public void ParseArgumentsReturnsDictionaryOfArgsKeyedByFlagBeyondASingleArg()
    {
      string[] args = CreateTestArgs();

      var parsedArgs = _testArgParser.ParseArguments(args);

      Assert.NotNull(parsedArgs);
      Assert.Equal(4, parsedArgs.Keys.Count);
      Assert.Equal("server_name", parsedArgs["-s"]);
      Assert.Equal("user_id", parsedArgs["-u"]);
      Assert.Equal("pwd", parsedArgs["-p"]);
    }

    [Fact]
    public void ValidateArgsReturnsFalseWhenArgsAreInvalid()
    {
      var invalidArgs = CreateValidArgs();
      invalidArgs.Remove("-d");

      var result = _testArgParser.ValidateArgs(invalidArgs);

      Assert.False(result);
    }

    [Fact]
    public void ValidateArgsReturnsTrueWhenArgsValid()
    {
      var validArgs = CreateValidArgs();

      var result = _testArgParser.ValidateArgs(validArgs);

      Assert.True(result);
    }

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