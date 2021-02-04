namespace ECommerce.Data.Request
{
    public class UpdateProductStockRequest
    {
        public UpdateProductStockRequest(string productCode, int stock)
        {
            ProductCode = productCode;
            Stock = stock;
        }
        public string ProductCode { get; set; }
        public int Stock { get; set; }
    }
}
