using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.ProductServiceTest
{
    public class GetProductInfoTest
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
        public void When_ProductCode_IsNull()
        {
            Assert.Throws<ValidationException>(() => productService.GetProductInfo(null));
        }
        [Test]
        public void When_Product_NotFound()
        {
            Assert.Throws<ProductNotFoundException>(() => productService.GetProductInfo("P1"));
        }

        [Test]
        public void When_GetProductInfo_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);

            var response = productService.GetProductInfo("P1");

            Assert.AreEqual(product.Price, response.Price);
            Assert.AreEqual(product.Stock, response.Stock);
            mockProductRepository.Verify(x => x.GetProduct(It.IsAny<string>()), Times.Once);
        }
    }
}
