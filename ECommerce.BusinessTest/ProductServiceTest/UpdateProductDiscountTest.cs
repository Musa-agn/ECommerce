using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.ProductServiceTest
{
    public class UpdateProductDiscountTest
    {
        ProductService productService;
        Mock<IProductRepository> mockProductRepository;
        Mock<ICampaignService> mockCampaignService;
        [SetUp]
        public void Setup()
        {
            mockProductRepository = new Mock<IProductRepository>();
            mockCampaignService = new Mock<ICampaignService>();
            productService = new ProductService(mockProductRepository.Object, mockCampaignService.Object);
        }
        [Test]
        public void When_Product_NotFound()
        {
            string productCode = "P1";
            decimal discount = 0;
            Assert.Throws<ProductNotFoundException>(() => productService.UpdateProductDiscount(productCode, discount));
        }
        [Test]
        public void When_UpdateProductStock_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            string productCode = "P1";
            decimal discount = 0;

            Assert.DoesNotThrow(() => productService.UpdateProductDiscount(productCode, discount));

            mockProductRepository.Verify(x => x.UpdateProductDiscount(It.IsAny<UpdateProductDiscountRequest>()), Times.Once());
        }
    }
}
