using System;
using System.Collections.Generic;

namespace XSqlClient
{
  public interface ISqlResult
  {
    List<string> ColumnNames { get; set; }
    List<Dictionary<string,object>> Rows { get; set; }
    string PrintDataToText();
  }
}