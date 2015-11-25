using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
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