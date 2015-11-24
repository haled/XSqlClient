using System;
using System.Collections.Generic;
using System.Text;

namespace XSqlClient
{
  public interface IArgumentParser
  {
    Dictionary<string,string> ParseArguments(string[] args);
    bool ValidateArgs(Dictionary<string,string> args);
  }
}