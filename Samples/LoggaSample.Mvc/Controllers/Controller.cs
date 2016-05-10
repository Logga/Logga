using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoggaSample.Mvc.Controllers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                return;

            Logga.LoggaService.CreateLogFromException(filterContext);
        }
    }
}