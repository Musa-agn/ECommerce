using System;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class BaseServiceException : Exception
    {
        public BaseServiceException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        public int StatusCode { get; set; }
    }
}
