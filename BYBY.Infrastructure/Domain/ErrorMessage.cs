using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Infrastructure.Domain
{
    public class ErrorMessage
    {


        public static ErrorMessage Create(ErrorType errorType,string message)
        {
            ErrorMessage info = new ErrorMessage();
            info.ErrorType = errorType;
            info.Message = message;
            return info;
        }
        public ErrorType ErrorType { get; set; }

        public string Message { get; set; }
    }
}
