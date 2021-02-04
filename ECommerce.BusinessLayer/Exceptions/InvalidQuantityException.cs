using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InvalidQuantityException : BaseServiceException
    {
        public InvalidQuantityException() : base("Quantity cannot be less than or equal to zero", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
