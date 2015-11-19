using System;
using System.Data.SqlClient;

namespace SqlUtility
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var connectionString = "Server=.;Database=<DBName>;User ID=<user>;Password=<pwd>;MultipleActiveResultSets=false";
      var sql = "SELECT * FROM <TableName>";

      using (var connection = new SqlConnection(connectionString))
      {
        connection.Open();
        var cmd = new SqlCommand(sql, connection);

        var reader = cmd.ExecuteReader();
        while(reader.Read())
        {
          Console.WriteLine("{0}|{1}|{2}", reader[0],reader[1],reader[2]);
        }
      }
    }
  }
}