// <fileinfo name="DbConnection.cs">
//		<copyright>
//			All rights reserved.
//		</copyright>
//		<remarks>
//			You can update this source code manually. If the file
//			already exists it will not be rewritten by the generator.
//		</remarks>
//		<generator rewritefile="False" infourl="http://www.SharpPower.com">RapTier</generator>
// </fileinfo>

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Project.Db
{
	/// <summary>
	/// Represents a connection to the <c>DbConnection</c> database.
	/// </summary>
	/// <remarks>
	/// If the <c>DbConnection</c> goes out of scope, the connection to the 
	/// database is not closed automatically. Therefore, you must explicitly close the 
	/// connection by calling the <c>Close</c> or <c>Dispose</c> method.
	/// </remarks>
	public class DbConnection : DbConnection_Base
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DbConnection"/> class.
		/// </summary>
		public DbConnection()
		{
			// EMPTY
		}

		/// <summary>
		/// Creates a new connection to the database.
		/// </summary>
		/// <returns>An <see cref="System.Data.SqlConnection"/> object.</returns>
		protected override SqlConnection CreateConnection()
		{

            //return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLDBConnString"].ConnectionString);

            return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["VMSConnection"]);//

            //return new System.Data.SqlClient.SqlConnection(["VMSConnection"]);
                        //return new System.Data.SqlClient.SqlConnection(
                        //    "Persist Security Info=False;User ID=sa;pwd=admin;Initia" +
                        //    "l Catalog=DineWine;Data Source=AIPL004\\AIPL004");
          
		}
		

		/// <summary>
		/// Creates a DataTable object for the specified command.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.SqlCommand"/> object.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected internal DataTable CreateDataTable(SqlCommand command)
		{
			DataTable dataTable = new DataTable();

			new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)command).Fill(dataTable);
		
			return dataTable;
		}

        /// <summary>
        /// Creates a DataSet object for the specified command.
        /// </summary>
        /// <param name="command">The <see cref="System.Data.SqlCommand"/> object.</param>
        /// <returns>A reference to the <see cref="System.Data.DataSet"/> object.</returns>
        protected internal DataSet CreateDataSet(SqlCommand command)
        {
            DataSet dataSet = new DataSet();
            System.Data.Common.DbDataAdapter dataAdapter;

            dataAdapter = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)command);

            dataAdapter.Fill(dataSet);
            return dataSet;
        } 

		/// <summary>
		/// Returns a SQL statement parameter name that is specific for the data provider.
		/// For example it returns ? for OleDb provider, or @paramName for MS SQL provider.
		/// </summary>
		/// <param name="paramName">The data provider neutral SQL parameter name.</param>
		/// <returns>The SQL statement parameter name.</returns>
		protected internal override string CreateSqlParameterName(string paramName)
		{

			return "@" + paramName;

		}

		/// <summary>
		/// Creates a .Net data provider specific parameter name that is used to
		/// create a parameter object and add it to the parameter collection of
		/// <see cref="System.Data.SqlCommand"/>.
		/// </summary>
		/// <param name="baseParamName">The base name of the parameter.</param>
		/// <returns>The full data provider specific parameter name.</returns>
		protected override string CreateCollectionParameterName(string baseParamName)
		{

			return "@" + baseParamName;

		}
	} // End of DbConnection class
} // End of namespace

