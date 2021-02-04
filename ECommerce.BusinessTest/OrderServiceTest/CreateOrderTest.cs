using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Dto.Output;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using Moq;
using NUnit.Framework;

namespace ECommerce.BusinessTest.OrderServiceTest
{
    public class CreateOrderTest
    {
        OrderService orderService;
        Mock<IOrderRepository> mockOrderRepository;
        Mock<IProductService> mockProductService;
        Mock<ICampaignService> mockCampaignService;
        [SetUp]
        public void Setup()
        {
            mockOrderRepository = new Mock<IOrderRepository>();
            mockProductService = new Mock<IProductService>();
            mockCampaignService = new Mock<ICampaignService>();
            orderService = new OrderService(mockOrderRepository.Object, mockProductService.Object, mockCampaignService.Object);
        }
        [Test]
        public void When_ProductCode_IsNull()
        {
            CreateOrderInput createOrderInput = new CreateOrderInput();
            Assert.Throws<ValidationException>(() => orderService.CreateOrder(createOrderInput));
        }
        [Test]
        public void When_Quantity_IsZero()
        {
            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = 0
            };
            Assert.Throws<InvalidQuantityException>(() => orderService.CreateOrder(createOrderInput));
        }
        [Test]
        public void When_Quantity_IsLessThanZero()
        {
            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = -10
            };
            Assert.Throws<InvalidQuantityException>(() => orderService.CreateOrder(createOrderInput));
        }

        [Test]
        public void When_Product_NotFound()
        {
            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = 10
            };
            Assert.Throws<ProductNotFoundException>(() => orderService.CreateOrder(createOrderInput));
        }

        [Test]
        public void When_Stock_IsZero()
        {
            GetProductInfoOutput getProductInfoOutput = new GetProductInfoOutput
            {
                Price = 100,
                Stock = 0
            };
            mockProductService.Setup(x => x.GetProductInfo("P1")).Returns(getProductInfoOutput);

            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = 10
            };

            Assert.Throws<InsufficientProductInStockException>(() => orderService.CreateOrder(createOrderInput));
        }
        [Test]
        public void When_Quantity_IsMoreThanStock()
        {
            GetProductInfoOutput getProductInfoOutput = new GetProductInfoOutput
            {
                Price = 100,
                Stock = 10
            };
            mockProductService.Setup(x => x.GetProductInfo("P1")).Returns(getProductInfoOutput);

            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = 20
            };

            Assert.Throws<InsufficientProductInStockException>(() => orderService.CreateOrder(createOrderInput));
        }


        [Test]
        public void When_CreateOrder_IsSuccessful()
        {
            Product product = new Product("P1", 100, 1000);
            GetProductInfoOutput getProductInfoOutput = new GetProductInfoOutput
            {
                Price = 100,
                Stock = 1000
            };
            mockProductService.Setup(x => x.GetProductInfo("P1")).Returns(getProductInfoOutput);

            CreateOrderInput createOrderInput = new CreateOrderInput
            {
                ProductCode = "P1",
                Quantity = 10
            };
            Assert.DoesNotThrow(() => orderService.CreateOrder(createOrderInput));
            mockOrderRepository.Verify(x => x.CreateOrder(It.IsAny<Order>()), Times.Once());
        }
    }
}
