using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.ProductServiceTest
{
    public class UpdateProductStockTest
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
        public void When_Quantity_IsZero()
        {
            UpdateProductStockInput input = new UpdateProductStockInput
            {
                ProductCode = "P1",
                Quantity = 0
            };
            Assert.Throws<InvalidQuantityException>(() => productService.UpdateProductStock(input));
        }
        [Test]
        public void When_Quantity_IsLessThanZero()
        {
            UpdateProductStockInput input = new UpdateProductStockInput
            {
                ProductCode = "P1",
                Quantity = -10
            };
            Assert.Throws<InvalidQuantityException>(() => productService.UpdateProductStock(input));
        }
        [Test]
        public void When_Product_NotFound()
        {
            UpdateProductStockInput input = new UpdateProductStockInput
            {
                ProductCode = "P1",
                Quantity = 10
            };
            Assert.Throws<ProductNotFoundException>(() => productService.UpdateProductStock(input));
        }
        [Test]
        public void When_UpdateProductStock_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);
            UpdateProductStockInput input = new UpdateProductStockInput
            {
                ProductCode = "P1",
                Quantity = 10
            };
            Assert.DoesNotThrow(() => productService.UpdateProductStock(input));
            mockProductRepository.Verify(x => x.UpdateProductStock(It.IsAny<UpdateProductStockRequest>()), Times.Once());
        }
    }
}
