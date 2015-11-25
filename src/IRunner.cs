using System;
using System.Collections.Generic;
using System.Text;

namespace XSqlClient
{
  public interface IRunner
  {
    string CreateConnectionString(Dictionary<string,string> args);
    //int Run(string[] args);
    string Usage();
    ISqlResult ExecuteQuery(string connectionString, string sql);
  }
}