using System;
using System.Collections.Generic;

namespace XSqlClient
{
  public class MockSqlResult : ISqlResult
  {
    public bool CalledPrintDataToText { get; set; }
    public string ExpectedTextOutput { get; set; }

    public List<string> ColumnNames { get; set; }
    public List<Dictionary<string,object>> Rows { get; set; }

    public MockSqlResult()
    {
      CalledPrintDataToText = false;

      ColumnNames = new List<string>();
      Rows = new List<Dictionary<string,object>>();
    }

    public string PrintDataToText()
    {
      CalledPrintDataToText = true;
      return ExpectedTextOutput;
    }
  }
}