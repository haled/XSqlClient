using System;
using System.Collections.Generic;
using System.Text;

namespace XSqlClient
{
  public class Runner
  {
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