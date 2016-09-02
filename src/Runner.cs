using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace XSqlClient
{
  public class Runner : IRunner
  {
    private const string USAGE_STRING = "Usage:\n\nXSqlClient <args>\n\n    -s <server_name>\n    -d <database_name>\n    -u <user_name>\n    -p <password>\n    -q <query>\n\n";

    public Runner()
    {
    }

    public string Usage()
    {
      return USAGE_STRING;
    }

    public string CreateConnectionString(Dictionary<string,string> args)
    {
      return string.Format("Server={0};Database={1};User ID={2};Password={3};MultipleActiveResultSets=false;",
                           args["-s"],args["-d"],args["-u"],args["-p"]);
    }

    public ISqlResult ExecuteQuery(string connectionString, string sql)
    {
      ISqlResult result = new SqlResult();
      bool gettingColumnNames = true;

      using (SqlConnection conn = new SqlConnection(connectionString))
      {
        conn.Open();
        var cmd = new SqlCommand(sql, conn);

        var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
          var newRow = new Dictionary<string,object>();
          for(int i = 0; i < reader.FieldCount; i++)
          {
            if(gettingColumnNames)
            {
              gettingColumnNames = i < reader.FieldCount;
              result.ColumnNames.Add(reader.GetName(i));
            }
            newRow.Add(reader.GetName(i), reader[i]);
          }
          result.Rows.Add(newRow);
        }
      }

      return result;
    }
  }
}