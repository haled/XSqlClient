using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace XSqlClient
{
  public class Runner : IRunner
  {
    private readonly string USAGE_STRING = "Usage:\n\nXSqlClient <args>\n\n    -s <server_name>\n    -d <database_name>\n    -u <user_name>\n    -p <password>\n    -q <query>\n\n";

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

    public string ExecuteQuery(string connectionString, string sql)
    {
      StringBuilder output = new StringBuilder();
      bool gettingColumnNames = true;
      List<string> columnNames = new List<string>();

      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        conn.Open();
        var cmd = new SqlCommand(sql, conn);

        var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
          for(int i = 0; i < reader.FieldCount; i++)
          {
            if(gettingColumnNames)
            {
              gettingColumnNames = i < reader.FieldCount;
              columnNames.Add(reader.GetName(i));
            }
            output.Append(CreateColumn(reader[i].ToString()));
          }
          output.Append("\n");
        }
      }

      var header = CreateHeaders(columnNames);

      return header + output.ToString();
    }

    private string CreateHeaders(List<string> columnNames)
    {
      var line1 = new StringBuilder();
      var line2 = new StringBuilder();

      foreach(var columnName in columnNames)
      {
        line1.Append(CreateColumn(columnName));
        line2.Append(CreateColumn("".PadLeft(columnName.Length, '-')));
      }
      line1.Append("\n");
      line1.Append(line2);
      line1.Append("\n");

      return line1.ToString();
    }

    private string CreateColumn(string value)
    {
      return value + "\t";
    }
  }
}