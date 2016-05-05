using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Logga
{
    public static class ConnectionConfiguration
    {
        private static string _connectionString;

        public static SqlConnection GetOpenConnection(string connectionStringOrName)
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

            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }


        internal static bool isConnectionString(string connectionStringOrName)
        {
            return connectionStringOrName.Contains(";");
        }

        internal static bool IsConnectionStringInConfiguration(string connectionStringName)
        {
            var connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            return connectionStringSetting != null;
        }
    }
}