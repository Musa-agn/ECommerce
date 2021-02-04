using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class InvalidTargetSaleCount : BaseServiceException
    {
        public InvalidTargetSaleCount() : base("Target sales cannot be less than or equal to zero", (int)HttpStatusCode.BadRequest)
        {
        }
    }
}
