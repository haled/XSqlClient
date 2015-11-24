using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
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