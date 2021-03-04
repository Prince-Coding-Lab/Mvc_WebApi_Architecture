using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Data
{
	public class DataAccessHelper : IDataAccessHelper, IDisposable
	{
		private SqlCommand command;
		private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DataDirectory}\Database.mdf;Integrated Security=True";
		public string GetConnection()
		{
			var connstr = ConnectionString.Replace("{DataDirectory}", HttpContext.Current.Server.MapPath("~/App_Data"));
			return connstr;
		}
		public void Command(string query)
		{
			//creating command object with connection name and proc name, and open connection for the command
			command = new SqlCommand(query, new SqlConnection(GetConnection()));
			command.CommandType = CommandType.Text;
			//command.Parameters.Add(
			//		new SqlParameter("ReturnValue",
			//				SqlDbType.Int,
			//		/* int size */ 4,
			//				ParameterDirection.ReturnValue,
			//		/* bool isNullable */ false,
			//		/* byte precision */ 0,
			//		/* byte scale */ 0,
			//		/* string srcColumn */ string.Empty,
			//				DataRowVersion.Default,
			//		/* value */ null
			//		)
			//);
			//command.CommandTimeout = 0;
			command.Connection.Open();
		}

		public void Dispose()
		{
			//	Dispose of this StoredProcedure.
			if (command != null)
			{
				SqlConnection connection = command.Connection;
				Debug.Assert(connection != null);
				connection.Close();
				command.Dispose();
				command = null;
				connection.Dispose();
			}
		}

		public async Task<int> RunAsync()
		{
			return await Task.Run(() =>
			{
				// Execute this stored procedure.  Int32 value returned by the stored procedure
				if (command == null)
					throw new ObjectDisposedException(GetType().FullName);
			return	command.ExecuteNonQuery();
				//return (int)command.Parameters["ReturnValue"].Value;
			});
		}

		public async Task<SqlDataReader> RunAsync(bool test)
		{
			if (command == null)
				throw new ObjectDisposedException(GetType().FullName);
			 return await command.ExecuteReaderAsync();
		}
	}
}
