using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IDataAccessHelper
	{
    Task<int> RunAsync();
    Task<SqlDataReader> RunAsync(bool test);
    //Task<int> RunAsync(DataTable dataTable);
    //Task<int> RunAsync(DataSet dataSet);
    void Command(string query);
    void Dispose();
  }
}
