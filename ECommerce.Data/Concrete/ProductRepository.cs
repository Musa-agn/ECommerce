using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using System.Linq;

namespace ECommerce.Data.Concrete
{
    public class ProductRepository : IProductRepository
    {
        public void CreateProduct(Product product)
        {
            ProductList.Products.Add(product);
        }

        public Product GetProduct(string productCode)
        {
            return ProductList.Products.FirstOrDefault(x => x.Code == productCode);
        }

        public void UpdateProductStock(UpdateProductStockRequest updateProductStockRequest)
        {
            var product = ProductList.Products.FirstOrDefault(x => x.Code == updateProductStockRequest.ProductCode);
            product.Stock = updateProductStockRequest.Stock;
        }

        public void UpdateProductDiscount(UpdateProductDiscountRequest updateProductDiscountRequest)
        {
            var product = ProductList.Products.FirstOrDefault(x => x.Code == updateProductDiscountRequest.ProductCode);
            product.Discount = updateProductDiscountRequest.Discount;
        }
    }
}
