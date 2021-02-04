using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InvalidStockException : BaseServiceException
    {
        public InvalidStockException() : base("Stock cannot be less than or equal to zero", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
