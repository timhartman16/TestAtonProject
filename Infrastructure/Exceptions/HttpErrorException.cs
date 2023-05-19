using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class HttpErrorException : Exception
    {
        public int StatusCode { get; set; } 
        public HttpErrorException(int statuscode, string message) : base(message) 
        {
            StatusCode = statuscode;
        }
    }
}
