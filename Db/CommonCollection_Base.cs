
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Caching;

namespace Project.Db
{

    /// <summary> 
    /// The base class for <see cref="CommonFunctionsCollection"/>. Provides methods 
    /// for common database table operations. 
    /// </summary> 
    /// <remarks> 
    /// Do not change this source code. Update the <see cref="CommonFunctionsCollection"/> 
    /// class if you need to add or change some functionality. 
    /// </remarks> 
    public abstract class CommonCollection_Base
    {
        // Constants 

        // Instance fields 
        private DbConnection _db;

        /// <summary> 
        /// Initializes a new instance of the <see cref="CommonFunctionsCollection_Base"/> 
        /// class with the specified <see cref="DbConnection"/>. 
        /// </summary> 
        /// <param name="db">The <see cref="DbConnection"/> object.</param> 
        public CommonCollection_Base(DbConnection db)
        {
            _db = db;
        }

        /// <summary> 
        /// Gets the database object that this table belongs to. 
        /// </summary> 
        /// <value>The <see cref="DbConnection"/> object.</value> 
        protected DbConnection Database
        {
            get { return _db; }
        }


        public DateTime GetServerDate()
        {
            DateTime ServerDate = DateTime.Now;
            string strSql = "SELECT CURRENT_TIMESTAMP as ServerDate";
            //String strSql = "SELECT convert(varchar,getdate(),101) as ServerDate"; 
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(strSql, "", "")))
            {
                while (reader.Read())
                {
                    ServerDate = Convert.ToDateTime(reader["ServerDate"]);

                }
            }
            return ServerDate;
        }

        public DataTable GetAsDataTable(string strSql, string whereSql, string orderBySql)
        {
            int totalRecordCount = -1;
            return GetAsDataTable(strSql, whereSql, orderBySql, 0, int.MaxValue, totalRecordCount);
        }
        public DataTable GetAsDataTable(string strSql, string whereSql, string orderBySql, bool xml)
        {
            int totalRecordCount = -1;
            return GetAsDataTable(strSql, whereSql, orderBySql, 0, int.MaxValue, totalRecordCount, xml);
        }     

        public virtual DataTable GetAsDataTable(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            DataTable dataTable = new DataTable();
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {
                    AddParameter(cmd, parametername[i].ToString(), values[i]);
                }
            }
            dataTable = _db.CreateDataTable(cmd);
            return dataTable;

        }
        public virtual DataTable GetAsDataTable(string procedurename, Hashtable parameterdetails)
        {
            ParameterDetails stParameterDetails;         
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            DataTable dataTable = new DataTable();      
            foreach (DictionaryEntry enParameterlist in parameterdetails) 
            {
                stParameterDetails =(ParameterDetails) enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(),stParameterDetails.Value, stParameterDetails.DataType);  
            }          
            dataTable = _db.CreateDataTable(cmd);
            return dataTable;

        }
       
        public virtual DataSet GetAsDataSet(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {            
            DataSet dataSet = new DataSet();
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {

                    AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }
            dataSet = _db.CreateDataSet(cmd);
            return dataSet;
        }
        public virtual DataSet GetAsDataSet(string procedurename, Hashtable parameterdetails)
        {
            ParameterDetails stParameterDetails;  
            DataSet dataSet = new DataSet();
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
             foreach (DictionaryEntry enParameterlist in parameterdetails) 
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);  
            }
            dataSet = _db.CreateDataSet(cmd);
            return dataSet;
        }
        public virtual DataTable GetAsDataTable(string strSql, string whereSql, string orderBySql, int startIndex, int length, int totalRecordCount)
        {
            DataTable dataTable = new DataTable();
            SqlCommand cmd = CreateGetCommand(strSql, whereSql, orderBySql);
            dataTable = _db.CreateDataTable(cmd);           
            return dataTable;
            
        }
        public virtual DataTable GetAsDataTable(string strSql, string whereSql, string orderBySql, int startIndex, int length,int totalRecordCount, bool xml)
        {          
            DataTable dataTable = new DataTable();
            SqlCommand cmd = CreateGetCommand(strSql, whereSql, orderBySql, xml);
            dataTable = _db.CreateDataTable(cmd);
            return dataTable;
        }   
        public virtual DataTable GetAsDataTable(string procedurename, string parametername, Int32 id)
        {
            DataTable dataTable = new DataTable();
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Length > 0)
            {
                AddParameter(cmd, parametername, id);
            }
            dataTable = _db.CreateDataTable(cmd);           
            return dataTable;
            
        }
       
        public virtual IDataReader GetRecordReader(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {

                    AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }
            IDataReader reader = _db.ExecuteReader(cmd);        
            return reader;
        }
        public virtual IDataReader GetRecordReader(string procedurename,Hashtable parameterdetails)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            ParameterDetails stParameterDetails;
           
            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }
            IDataReader reader = _db.ExecuteReader(cmd);    
            return reader;
        }
        public virtual bool UpdateRecord(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {
                  AddParameter(cmd, parametername[i].ToString(), values[i]);
                }
            }
            return 0 != cmd.ExecuteNonQuery();
        }



        public virtual bool UpdateRecord(string procedurename, Hashtable parameterdetails)
        {
            ParameterDetails stParameterDetails;
            DataTable dataTable = new DataTable();
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }
            
            return 0 != cmd.ExecuteNonQuery();
        }

        public virtual bool UpdateSqlRecord(string procedurename, Hashtable parameterdetails)
        {
            SqlParameterDetails stParameterDetails;
            DataTable dataTable = new DataTable();
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (SqlParameterDetails)enParameterlist.Value;
                AddSqlParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }

            return 0 != cmd.ExecuteNonQuery();
        }




        public virtual string InsertRecord(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);         
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {

                    AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }
           return  Convert.ToString(cmd.ExecuteScalar());
           
        }

        public virtual string InsertRecord(string procedurename, Hashtable parameterdetails)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true); 
            ParameterDetails stParameterDetails;
            
            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }

            return Convert.ToString(cmd.ExecuteScalar());

        }

        public virtual string InsertSqlRecord(string procedurename, Hashtable parameterdetails)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            SqlParameterDetails stParameterDetails;

            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (SqlParameterDetails)enParameterlist.Value;
                AddSqlParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }

            return Convert.ToString(cmd.ExecuteScalar());

        }


        public virtual bool DeleteRecord(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {

                    AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }
            return 0 != cmd.ExecuteNonQuery();
        }

        public virtual bool DeleteRecord(string procedurename, Hashtable parameterdetails)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            ParameterDetails stParameterDetails;

            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }

            return 0 != cmd.ExecuteNonQuery();
        }
        public virtual bool SetRecordValue(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {

                    AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }
            return 0 != cmd.ExecuteNonQuery();
        }
        public bool GetRecordStatus(string strTableName, string strwhereSql, bool procedure)
        {
            string strSql = "SELECT COUNT(*) FROM " + strTableName + "";
            if (null != strwhereSql && 0 < strwhereSql.Length)
                strSql += " WHERE " + strwhereSql;

            SqlCommand cmd = _db.CreateCommand(strSql, procedure);
            // AddParameter(cmd, "@UserName", strUserName);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }      
        public bool GetRecordStatus(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {
                   AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetRecordStatus(string procedurename, Hashtable parameterdetails)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);         
            ParameterDetails stParameterDetails;

            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public object GetRecordObject(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {

                    AddParameter(cmd, parametername[i].ToString(), values[i]);

                }
            }

            object result = cmd.ExecuteScalar();

            return result;

        }

        public object GetRecordObject(string procedurename, Hashtable parameterdetails)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            ParameterDetails stParameterDetails;

            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }
            object result = cmd.ExecuteScalar();

            return result;
        }
        public Int32 GetRecordCount(string strSql, bool procedure)
        {

            SqlCommand cmd = _db.CreateCommand(strSql, procedure);
            // AddParameter(cmd, "@UserName", strUserName); 
            int count = (int)cmd.ExecuteScalar();
            return count;

        }
        public Int32 GetRecordCount(string procedurename, Hashtable parameterdetails)
        {

            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            ParameterDetails stParameterDetails;
            foreach (DictionaryEntry enParameterlist in parameterdetails)
            {
                stParameterDetails = (ParameterDetails)enParameterlist.Value;
                AddParameter(cmd, enParameterlist.Key.
                    ToString(), stParameterDetails.Value, stParameterDetails.DataType);
            }
            // AddParameter(cmd, "@UserName", strUserName); 
            int count = (int)cmd.ExecuteScalar();
            return count;
        }
        public Int32 GetRecordCount(string procedurename, System.Collections.ArrayList parametername, System.Collections.ArrayList values)
        {
            SqlCommand cmd = _db.CreateCommand(procedurename, true);
            if (parametername.Count > 0)
            {
                for (int i = 0; i <= parametername.Count - 1; i++)
                {
                    AddParameter(cmd, parametername[i].ToString(), values[i]);
                }
            }
            Int32 count = (Int32)cmd.ExecuteScalar();
            return count;
        }       
        protected virtual SqlParameter AddParameter(SqlCommand cmd, string paramName, object value,SqlDbType datatype)
        {
            SqlParameter parameter;

            parameter = _db.AddParameter(cmd, paramName, datatype, value);

            return parameter;
        }
        protected virtual SqlParameter AddSqlParameter(SqlCommand cmd, string paramName, object value, SqlDbType datatype)
        {
            SqlParameter parameter;

            parameter = _db.AddSqlParameter(cmd, paramName, datatype, value);

            return parameter;
        }
        protected virtual SqlParameter AddParameter(SqlCommand cmd, string paramName, object value)
        {
            SqlParameter parameter;
            switch (paramName)
            {
                case "UserID":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.Int, value);
                    break;
                case "UserName":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Password":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "ModuleID":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.Int, value);
                    break;
                case "FormName":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "FormID":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.Int, value);
                    break;
                case "PKName":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "PKvalue":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "FieldName":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "param":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "TableName":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "tablename":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Uniquekeycolumn":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "UniquekeycolumnValue":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "EditMode":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Difference":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "ClientId":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Col1":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Col2":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "FilterId":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "SearchTable":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Str":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "InsertStr":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "PKTable":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "ListBoxValues":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "PKIDvalue":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "PKField":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "ListBoxTable":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "SearchColumns":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "SearchStr":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Fieldname":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "Fieldval":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;
                case "ModuleId":
                    parameter = _db.AddParameter(cmd, paramName, SqlDbType.VarChar, value);
                    break;

                default:
                    throw new ArgumentException("Unknown parameter name (" + paramName + ").");

            }

            return parameter;
        }
        /// <summary> 
        /// Creates an <see cref="System.Data.IDbCommand"/> object for the specified search criteria. 
        /// </summary> 
        /// <param name="whereSql">The SQL search condition. For example: "FirstName='XYZ' AND Code=10001".</param> 
        /// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending). 
        /// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param> 
        /// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns> 
        /// 
        protected virtual SqlCommand CreateGetCommand(string strSql, string whereSql, string orderBySql)
        {
            string sql = strSql;
            if (whereSql != null && 0 < whereSql.Length)
            {
                sql += " WHERE " + whereSql;
            }
            if (orderBySql != null && 0 < orderBySql.Length)
            {
                sql += " ORDER BY " + orderBySql;
            }
            return _db.CreateCommand(sql);

        }
        protected virtual SqlCommand CreateGetCommand(string strSql, string whereSql, string orderBySql, bool xml)
        {
            string sql = strSql;
            if (whereSql != null && 0 < whereSql.Length)
            {
                sql += " WHERE " + whereSql;
            }
            if (orderBySql != null && 0 < orderBySql.Length)
            {
                sql += " ORDER BY " + orderBySql;
            }
            if (xml == true)
            {
                sql += " for xml auto";
            }
            return _db.CreateCommand(sql);

        }
        /// <summary> 
        /// Deletes <c>PFS_INETERESTS_DET</c> records that match the specified criteria. 
        /// </summary> 
        /// <param name="whereSql">The SQL search condition. 
        /// For example: <c>"FirstName='XYZ' AND Code=10001"</c>.</param> 
        /// <returns>The number of deleted records.</returns> 
        public int Delete(string tablename, string whereSql)
        {
            return CreateDeleteCommand(tablename, whereSql).ExecuteNonQuery();
        }

        /// <summary> 
        /// Creates an <see cref="System.Data.SqlCommand"/> object that can be used 
        /// to delete <c>PFS_INETERESTS_DET</c> records that match the specified criteria. 
        /// </summary> 
        /// <param name="whereSql">The SQL search condition. 
        /// For example: <c>"FirstName='XYZ' AND Code=10001"</c>.</param> 
        /// <returns>A reference to the <see cref="System.Data.SqlCommand"/> object.</returns> 
        protected virtual SqlCommand CreateDeleteCommand(string tablename, string whereSql)
        {
            string sql = "DELETE FROM [dbo].[" + tablename + "]";
            if (whereSql != null && 0 < whereSql.Length)
            {
                sql += " WHERE " + whereSql;
            }
            return _db.CreateCommand(sql);

            //return _db.CreateCommand("QMSA_DeleteRecords", true).ExecuteNonQuery(); 
        }
    }

// End of CommonFunctionCollection_Base class 
//End Namespace 
}