using System;
using System.Collections.Generic;
using System.Text;

namespace XSqlClient
{
  public class Runner
  {
    public Dictionary<string,string> ParseArguments(string[] args)
    {
      Dictionary<string,string> result = new Dictionary<string,string>();

      if(args == null) return null;

      if(args.Length % 2 != 0)
      {
        throw new ArgumentException("Arguments must be passed with a flag and a value. (Must have an even number of args.)");
      }

      for(int i = 0; i < args.Length; i=i+2)
      {
        result.Add(args[i],args[i+1]);
      }

      return result;
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