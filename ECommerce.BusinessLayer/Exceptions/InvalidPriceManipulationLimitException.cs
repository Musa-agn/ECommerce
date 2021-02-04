using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InvalidPriceManipulationLimitException : BaseServiceException
    {
        public InvalidPriceManipulationLimitException(string message) : base(message, (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
