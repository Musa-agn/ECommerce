using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Dto.Output;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface IProductService
    {
        void CreateProduct(CreateProductInput createProductInput);
        GetProductInfoOutput GetProductInfo(string productCode);
        void UpdateProductStock(UpdateProductStockInput updateProductStockInput);
        void UpdateProductDiscount(string productCode, decimal discount);
    }
}
