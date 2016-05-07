using Logga.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
using System.Web.Http.Filters;
using Logga.Data;

namespace Logga
{
    public class LoggaService
    {
        public static void CreateLogFromContext(HttpContext context)
        {
            using (var connection = ConnectionConfiguration.GetOpenConnection(LoggaConfiguration._connectionString))
            {
                var error = new LoggaEntry
                {
                    // ErrorLogId = Guid.NewGuid(),
                    Source = context.Error.Source,
                    Target = context.Request.Url.ToString(),
                    Type = context.Error.GetType().Name,
                    Message = context.Error.Message,
                    StackTrace = context.Error.StackTrace,
                    DateError = DateTime.Now,
                    Host = context.Server.MachineName,
                    InnerException = context.Error.InnerException != null ? context.Error.InnerException.ToString() : "null"
                };

                if (context.User.Identity.IsAuthenticated)
                {
                    error.User = context.User.Identity.Name;
                };

                string processQuery = "INSERT INTO LoggaEntry VALUES (@DateError, @Source, @Target, @Type, @Message, @StackTrace, @User, @Host, @InnerException)";
                connection.Execute(processQuery, error);
            }
        }

        public static void CreateLogFromException(ExceptionContext exception)
        {
            using (var connection = ConnectionConfiguration.GetOpenConnection(LoggaConfiguration._connectionString))
            {
                var error = new LoggaEntry
                {
                    // ErrorLogId = Guid.NewGuid(),
                    Source = exception.Exception.Source,
                    Target = exception.HttpContext.Request.Url.ToString(),
                    Type = exception.Exception.GetType().Name,
                    Message = exception.Exception.Message,
                    StackTrace = exception.Exception.StackTrace,
                    DateError = DateTime.Now,
                    Host = exception.HttpContext.Server.MachineName,
                    InnerException = exception.Exception.InnerException != null ? exception.Exception.InnerException.ToString() : "null"
                };

                if (exception.HttpContext.User.Identity.IsAuthenticated)
                {
                    error.User = exception.HttpContext.User.Identity.Name;
                };

                string processQuery = "INSERT INTO LoggaEntry VALUES (@DateError, @Source, @Target, @Type, @Message, @StackTrace, @User, @Host, @InnerException)";
                connection.Execute(processQuery, error);
            }
        }

        public static void CreateLogFromApiException(HttpActionExecutedContext context)
        {
            using (var connection = ConnectionConfiguration.GetOpenConnection(LoggaConfiguration._connectionString))
            {
                var error = new LoggaEntry
                {
                    // ErrorLogId = Guid.NewGuid(),
                    Source = context.Exception.Source,
                    Target = context.Request.RequestUri.ToString(),
                    Type = context.Exception.GetType().Name,
                    Message = context.Exception.Message,
                    StackTrace = context.Exception.StackTrace,
                    DateError = DateTime.Now,
                    Host = ((HttpContextWrapper)context.Request.Properties["MS_HttpContext"]).Request.UserHostName.ToString(),
                    InnerException = context.Exception.InnerException != null ? context.Exception.InnerException.ToString() : "null"
                };

                if (context.ActionContext.ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    error.User = context.ActionContext.ControllerContext.RequestContext.Principal.Identity.Name;
                };

                string processQuery = "INSERT INTO LoggaEntry VALUES (@DateError, @Source, @Target, @Type, @Message, @StackTrace, @User, @Host, @InnerException)";
                connection.Execute(processQuery, error);
            }
        }

        public static void SaveLog(LoggaEntry entry)
        {
            using (var connection = ConnectionConfiguration.GetOpenConnection(LoggaConfiguration._connectionString))
            {
                string processQuery = "INSERT INTO LoggaEntry VALUES (@DateError, @Source, @Target, @Type, @Message, @StackTrace, @User, @Host, @InnerException)";
                connection.Execute(processQuery, entry);
            }

        }
    }
}