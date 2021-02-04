namespace ECommerce.BusinessLayer.Dto.Input
{
    public class CreateProductInput
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
