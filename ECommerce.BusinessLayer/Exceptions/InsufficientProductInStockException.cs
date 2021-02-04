using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InsufficientProductInStockException : BaseServiceException
    {
        public InsufficientProductInStockException() : base("Insufficient product in stock", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
