using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Dapper;
using System.Reflection;
using System.IO;
using System.Data;

namespace Logga.Data.SqlServer
{
    public class UseSqlServerData
    {
        private readonly SqlConnection _connection;
        private readonly string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringOrName"></param>
        /// <param name="installSchema"></param>
        public UseSqlServerData(string connectionStringOrName, bool installSchema = true)
        {
            if (connectionStringOrName == null) throw new ArgumentNullException("connectionStringOrName");

            if (IsConnectionStringInConfiguration(connectionStringOrName))
            {
                _connectionString = ConfigurationManager.ConnectionStrings[connectionStringOrName].ConnectionString;
            }
            else if (isConnectionString(connectionStringOrName))
            {
                _connectionString = connectionStringOrName;
            }
            else
            {
                throw new ArgumentException(
                    string.Format("Could not find connection string with name '{0}' in application config file",
                                  connectionStringOrName));
            }

            if (installSchema)
            {
                CheckDatabaseExists();
                using (var connection = GetOpenConnection())
                {
                    Install(connection);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringOrName"></param>
        /// <returns></returns>
        internal bool isConnectionString(string connectionStringOrName)
        {
            return connectionStringOrName.Contains(";");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        internal bool IsConnectionStringInConfiguration(string connectionStringName)
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            return connectionStringSetting != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal SqlConnection GetOpenConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        internal void Install(SqlConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            var script = GetTextFomFile(typeof(UseSqlServerData).Assembly,
                "Logga.Data.SqlServer.Install.sql");

            connection.Execute(script);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        internal string GetTextFomFile(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(String.Format(
                        "The file {0} not found in resource {1}`.",
                        resourceName,
                        assembly));
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Check if database exists. If not, create a new database.
        /// </summary>
        internal void CheckDatabaseExists()
        {
            try
            {
                using (var connection = new SqlConnection("Server=.\\SQLEXPRESS;Integrated security=SSPI;database=master"))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT db_id('Logga')", connection);
                    var test = command.ExecuteScalar();

                    if (test == DBNull.Value)
                    {
                        var createDatabaseCommand = new SqlCommand("CREATE DATABASE Logga", connection);

                        createDatabaseCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        //public static SqlConnection GetOpenConnection(string conn)
        //{
        //    if (conn == null) throw new ArgumentNullException("connectionStringOrName");

        //    if (IsConnectionStringInConfiguration(conn))
        //    {
        //        _connectionString = ConfigurationManager.ConnectionStrings[connectionStringOrName].ConnectionString;
        //    }
        //    else if (isConnectionString(connectionStringOrName))
        //    {
        //        _connectionString = connectionStringOrName;
        //    }

        //    var connection = new SqlConnection(_connectionString);
        //    connection.Open();

        //    return connection;
        //}
    }
}
