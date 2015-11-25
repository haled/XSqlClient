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
          if(gettingColumnNames)
          {
            gettingColumnNames = false;
            for(int j = 0; j < reader.FieldCount; j++)
            {
              columnNames.Add(reader.GetName(j));
            }

            foreach(var columnName in columnNames)
            {
              output.Append(columnName);
              output.Append("\t");
            }
            output.Append("\n");

            foreach(var columnName in columnNames)
            {
              output.Append("".PadLeft(columnName.Length, '-'));
              output.Append("\t");
            }
            output.Append("\n");
          }

          for(int i = 0; i < reader.FieldCount; i++)
          {
            output.Append(reader[i]);
            output.Append("\t");
          }
          output.Append("\n");
        }
      }

      return output.ToString();
    }
  }
}