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

namespace Logga.Data.SqlServer
{
    public class UseSqlServerData
    {
        private readonly SqlConnection _connection;
        private readonly string _connectionString;


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
                using (var connection = GetOpenConnection())
                {
                    Install(connection);
                }
            }
        }

        internal bool isConnectionString(string connectionStringOrName)
        {
            return connectionStringOrName.Contains(";");
        }

        internal bool IsConnectionStringInConfiguration(string connectionStringName)
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            return connectionStringSetting != null;
        }

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

        internal void Install(SqlConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            var script = GetTextFomFile(typeof(UseSqlServerData).Assembly,
                "Logga.Data.SqlServer.Install.sql");

            connection.Execute(script);

        }

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
