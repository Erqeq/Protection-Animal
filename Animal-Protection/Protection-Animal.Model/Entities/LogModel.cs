using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protection_Animal.Model.Entities
{
    public class LogModel
    {
        public int Id { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
    }
}
