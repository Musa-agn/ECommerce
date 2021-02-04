using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.ProductServiceTest
{
    public class CreateProductTest
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
            CreateProductInput createProductInput = new CreateProductInput();
            Assert.Throws<ValidationException>(() => productService.CreateProduct(createProductInput));
        }
        [Test]
        public void When_ProductCode_IsUsed()
        {
            Product product = new Product("P1", 100, 1000);
            mockProductRepository.Setup(x => x.GetProduct("P1")).Returns(product);

            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Price = 100,
                Stock = 1000
            };

            Assert.Throws<BaseServiceException>(() => productService.CreateProduct(createProductInput));
        }
        [Test]
        public void When_Price_IsZero()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Stock = 100,
                Price = 0
            };
            Assert.Throws<InvalidPriceException>(() => productService.CreateProduct(createProductInput));
        }
        [Test]
        public void When_Price_IsLessThan()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Stock = 100,
                Price = -10
            };
            Assert.Throws<InvalidPriceException>(() => productService.CreateProduct(createProductInput));
        }
        [Test]
        public void When_Stock_IsZero()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Price = 100,
                Stock = 0,
            };
            Assert.Throws<InvalidStockException>(() => productService.CreateProduct(createProductInput));
        }
        [Test]
        public void When_Stock_IsLessThanZero()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Price = 100,
                Stock = -10,
            };
            Assert.Throws<InvalidStockException>(() => productService.CreateProduct(createProductInput));
        }

        [Test]
        public void When_CreateProduct_IsSuccessful()
        {
            CreateProductInput createProductInput = new CreateProductInput
            {
                Code = "P1",
                Price = 100,
                Stock = 1000,
            };
            Assert.DoesNotThrow(() => productService.CreateProduct(createProductInput));
            mockProductRepository.Verify(x => x.CreateProduct(It.IsAny<Product>()), Times.Once());
        }
    }
}
