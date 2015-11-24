using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class ProgramTests
  {
    private IRunner _mockRunner;
    private IArgumentParser _mockArgParser;
    private Program _testProgram;

    public ProgramTests()
    {
      _mockArgParser = new MockArgumentParser();
      _mockRunner = new MockRunner();
      _testProgram = new Program(_mockArgParser, _mockRunner);
    }

    [Fact]
    public void RunReturnsZeroExitCodeWithValidArguments()
    {
      string[] args = new string[4];
      Dictionary<string,string> fakeArgs = new Dictionary<string,string>();
      bool fakeValidationResult = true;
      ((MockArgumentParser)_mockArgParser).MockParsedArgs = fakeArgs;
      ((MockArgumentParser)_mockArgParser).ExpectedValidationResult = fakeValidationResult;

      var exitCode = _testProgram.Run(args);

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

      var exitCode = _testProgram.Run(args);

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

  public class MockRunner : IRunner
  {
    public string CreateConnectionString(Dictionary<string,string> args)
    {
      return "";
    }

    public string Usage()
    {
      return "";
    }
  }
}