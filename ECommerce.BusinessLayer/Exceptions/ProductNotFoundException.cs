using System.Net;

namespace ECommerce.BusinessLayer.Exceptions
{
    public class ProductNotFoundException : BaseServiceException
    {
        public ProductNotFoundException() : base("Product not found", (int)HttpStatusCode.NotFound)
        {
        }
    }
}
