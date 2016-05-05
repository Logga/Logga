using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logga.Models
{
    [Table("ErrorLogs")]
    public class ErrorLogEntry
    {
        [Key]
        public long ErrorLogId { get; set; }

        [DisplayName("Date")]
        public DateTime DateError { get; set; }
        [DisplayName("Source")]
        public String Source { get; set; }
        [DisplayName("Target")]
        public String Target { get; set; }
        [DisplayName("Type")]
        public String Type { get; set; }
        [DisplayName("Message")]
        public String Message { get; set; }
        [DisplayName("Stack Trace")]
        public String StackTrace { get; set; }
        [DisplayName("User")]
        public String User { get; set; }
        [DisplayName("Host")]
        public String Host { get; set; }
        [DisplayName("Inner Exception")]
        public String InnerException { get; set; }
    }
}
