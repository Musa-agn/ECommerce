using ECommerce.Data.Entity;
using ECommerce.Data.Request;

namespace ECommerce.Data.Abstract
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProduct(string productCode);
        void UpdateProductStock(UpdateProductStockRequest updateProductStockRequest);
        void UpdateProductDiscount(UpdateProductDiscountRequest updateProductDiscountRequest);
    }
}
