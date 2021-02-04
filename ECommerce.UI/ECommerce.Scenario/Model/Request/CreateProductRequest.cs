namespace ECommerce.Scenario.Model.Request
{
    public class CreateProductRequest
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
