using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InvalidPriceException : BaseServiceException
    {
        public InvalidPriceException() : base("Price cannot be less than or equal to zero", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
