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
  }

  public class Runner : IRunner
  {
    private readonly string USAGE_STRING = "Usage:\n\nXSqlClient <args>\n\n    -s <server_name>\n    -d <database_name>\n    -u <user_name>\n    -p <password>\n\n";

    public Runner()
    {
    }

    public string Usage()
    {
      return USAGE_STRING;
    }

    public string CreateConnectionString(Dictionary<string,string> args)
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Server=");
      sb.Append(args["-s"]);
      sb.Append(";Database=");
      sb.Append(args["-d"]);
      sb.Append(";User ID=");
      sb.Append(args["-u"]);
      sb.Append(";Password=");
      sb.Append(args["-p"]);
      sb.Append(";MultipleActiveResultSets=false;");
      return sb.ToString();
    }


  }
}