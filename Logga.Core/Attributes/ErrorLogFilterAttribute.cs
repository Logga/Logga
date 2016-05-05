using Logga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace ErrorLog.Core.Attributes
{
    public class ErrorLogFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context == null)
                return;

            LoggaService.CreateLogFromApiException(context);
        }
    }
}