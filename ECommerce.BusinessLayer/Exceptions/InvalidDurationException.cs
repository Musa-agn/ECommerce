using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InvalidDurationException : BaseServiceException
    {
        public InvalidDurationException() : base("Duration cannot be equal to or less than zero", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
