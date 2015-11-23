using System;
using System.Collections.Generic;
using System.Text;

namespace XSqlClient
{
  public class ArgumentParser
  {
    private Dictionary<string,string> _definedArguments;

    public ArgumentParser(Dictionary<string,string> desiredArgs)
    {
      _definedArguments = desiredArgs;
    }

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

    public bool ValidateArgs(Dictionary<string,string> args)
    {
      bool result = true;

      foreach(string key in _definedArguments.Keys)
      {
        result = result && args.ContainsKey(key);
      }

      return result;
    }
  }
}