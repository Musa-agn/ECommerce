using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class ValidationException : BaseServiceException
    {
        public ValidationException(string propertyName) : base(propertyName + " is invalid", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
