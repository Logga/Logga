using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LoggaSample.Api.Controllers
{
    public class ErrorController : ApiController
    {
        // GET api/error
        public IEnumerable<string> Get()
        {
            throw new Exception("This is a Web API Exception, caught by Logga via Filter Attribute. Now check http://" + Request.RequestUri.Authority + "/Logga for the list of log messages.");
        }
    }
}
