using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dapper;
using Logga.Data;

namespace Logga.Controllers
{
    public class LoggaController : Controller
    {
         public ActionResult CreateError()
        {
            throw new Exception("Hi, I'm Logga and i'm your friend!");
        }

        public ActionResult Index()
        {
            using (var connection = ConnectionConfiguration.GetOpenConnection(LoggaConfiguration._connectionString))
            {
                var loggaList = connection.Query<LoggaEntry>("SELECT * FROM LoggaEntry");

                return View(loggaList);
            }
        }

        public ActionResult Details(int id)
        {
            using (var connection = ConnectionConfiguration.GetOpenConnection(LoggaConfiguration._connectionString))
            {
                var logga = connection.Query<LoggaEntry>("SELECT * FROM LoggaEntry WHERE LoggaEntryId = @id", new { id = id}).FirstOrDefault();

                return View(logga);
            }

        }
    }
}
