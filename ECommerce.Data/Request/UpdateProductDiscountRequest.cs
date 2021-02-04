namespace ECommerce.Data.Request
{
    public class UpdateProductDiscountRequest
    {
        public UpdateProductDiscountRequest(string productCode, decimal discount)
        {
            ProductCode = productCode;
            Discount = discount;
        }
        public string ProductCode { get; set; }
        public decimal Discount { get; set; }
    }
}
