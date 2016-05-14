using Logga.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logga
{
    public class LoggaConfiguration
    {
        public static String _connectionString;

        public static void UseSqlServerData(String connectionString, bool installSchema = true, bool createNewDatabase = false)
        {
            var sql = new UseSqlServerData(connectionString, installSchema, createNewDatabase);
            _connectionString = connectionString;
        }
    }
}