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
      string[] args = new string[5];
      Dictionary<string,string> fakeArgs = CreateValidArgs();
      bool fakeValidationResult = true;
      ((MockArgumentParser)_mockArgParser).MockParsedArgs = fakeArgs;
      ((MockArgumentParser)_mockArgParser).ExpectedValidationResult = fakeValidationResult;

      var exitCode = _testProgram.Run(args);

      Assert.True(((MockArgumentParser)_mockArgParser).CalledParseArguments);
      Assert.True(((MockArgumentParser)_mockArgParser).CalledValidateArgs);
      Assert.True(((MockRunner)_mockRunner).CalledCreateConnectionString);
      Assert.True(((MockRunner)_mockRunner).CalledExecuteQuery);

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
        {"-p","pwd"},
        {"-q","query"}
      };
    }
  }
}