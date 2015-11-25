using System;
using System.Collections.Generic;
using Xunit;

namespace XSqlClient
{
  public class SqlResultTests
  {
    [Fact]
    public void EmptyStringReturnedWithEmptyObject()
    {
      var _testResult = new SqlResult();

      var output = _testResult.PrintDataToText();

      Assert.Equal("",output);
    }

    [Fact]
    public void ColumnsWithNoRowsReturnsEmptyString()
    {
      var _testResult = new SqlResult();
      _testResult.ColumnNames.Add("col1");

      var output = _testResult.PrintDataToText();

      Assert.Equal("",output);
    }

    [Fact]
    public void RowsWithNoColumnNamesThrowsAnException()
    {
      var _testResult = new SqlResult();
      _testResult.Rows.Add(new Dictionary<string, object> {{"test","test"}});

      var result = Record.Exception(() => _testResult.PrintDataToText());

      Assert.NotNull(result);
      Assert.IsType<Exception>(result);
      Assert.Equal("No column names were provided, so a result can't be displayed.", result.Message);
    }

    [Fact]
    public void ValidDataReturnsFormattedOutput()
    {
      var _testResult = new SqlResult();
      _testResult.ColumnNames.Add("col1");
      _testResult.Rows.Add(new Dictionary<string, object> {{"col1","test"}});

      var output = _testResult.PrintDataToText();

      Assert.Equal("col1    \n----    \ntest    \n",output);
    }
  }
}