using System;
using System.Collections.Generic;
using System.Text;

namespace XSqlClient
{
  public class SqlResult : ISqlResult
  {
    public List<string> ColumnNames { get; set; }
    public List<Dictionary<string,object>> Rows { get; set; }

    public SqlResult()
    {
      ColumnNames = new List<string>();
      Rows = new List<Dictionary<string,object>>();
    }

    private string CreateHeaderRow()
    {
      var line1 = new StringBuilder();
      var line2 = new StringBuilder();

      foreach(var columnName in ColumnNames)
      {
        line1.Append(CreateColumn(columnName));
        line2.Append(CreateColumn("".PadLeft(columnName.Length, '-')));
      }

      if(line1.Length > 0)
      {
        line1.AppendLine();
        line1.Append(line2);
        line1.AppendLine();
      }

      return line1.ToString();
    }

    public string PrintDataToText()
    {
      StringBuilder output = new StringBuilder();
      
      foreach(var row in Rows)
      {
        foreach(var columnName in ColumnNames)
        {
          output.Append(CreateColumn(row[columnName].ToString()));
        }
        output.AppendLine();
      }

      if(output.Length > 0)
      {
        var header = CreateHeaderRow();
        if(header.Length <= 0)
        {
          throw new Exception("No column names were provided, so a result can't be displayed.");
        }
        output.Insert(0, header);
      }

      return output.ToString();
    }

    private string CreateColumn(string value)
    {
      return value + "    ";
    }
  }
}