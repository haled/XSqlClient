using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class MockRunner : IRunner
  {
    public ISqlResult ExpectedResultData { get; set; }
    public bool CalledCreateConnectionString { get; set; }
    public bool CalledUsage { get; set; }
    public bool CalledExecuteQuery { get; set; }

    public MockRunner()
    {
      CalledCreateConnectionString = false;
      CalledUsage = false;
      CalledExecuteQuery = false;
    }

    public string CreateConnectionString(Dictionary<string,string> args)
    {
      CalledCreateConnectionString = true;
      return "some string";
    }

    public string Usage()
    {
      CalledUsage = true;
      return "usage";
    }

    public ISqlResult ExecuteQuery(string connectionString, string sql)
    {
      CalledExecuteQuery = true;
      return ExpectedResultData;
    }
  }
}