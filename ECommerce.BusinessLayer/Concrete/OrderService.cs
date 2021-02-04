using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using System;

namespace ECommerce.BusinessLayer.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly ICampaignService _campaignService;

        public OrderService(IOrderRepository orderRepository,
             IProductService productService,
            ICampaignService campaignService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _campaignService = campaignService;
        }

        public void CreateOrder(CreateOrderInput createOrderInput)
        {
            CreateOrderInputValidate(createOrderInput);
            var productInfo = _productService.GetProductInfo(createOrderInput.ProductCode);
            decimal price = productInfo.Price * createOrderInput.Quantity;

            _orderRepository.CreateOrder(new Order(createOrderInput.ProductCode, createOrderInput.Quantity, price, productInfo.CampaignId));

            UpdateProductStock(createOrderInput);
            if (productInfo.CampaignId.HasValue)
            {
                UpdateCampaignInfo(productInfo.CampaignId.Value, createOrderInput, price);
            }
        }

        private void UpdateProductStock(CreateOrderInput createOrderInput)
        {
            _productService.UpdateProductStock(new UpdateProductStockInput
            {
                ProductCode = createOrderInput.ProductCode,
                Quantity = createOrderInput.Quantity
            });
        }

        private void UpdateCampaignInfo(Guid campaignId, CreateOrderInput createOrderInput, decimal price)
        {
            _campaignService.UpdateCampaignInfo(new UpdateCampaignInfoInput
            {
                CampaignId = campaignId,
                Quantity = createOrderInput.Quantity,
                ProductCode = createOrderInput.ProductCode,
                Price = price
            });
        }
        private void CreateOrderInputValidate(CreateOrderInput createOrderInput)
        {
            if (string.IsNullOrWhiteSpace(createOrderInput.ProductCode))
            {
                throw new ValidationException(nameof(createOrderInput.ProductCode));
            }
            if (createOrderInput.Quantity <= 0)
            {
                throw new InvalidQuantityException();
            }
            var productInfo = _productService.GetProductInfo(createOrderInput.ProductCode);
            if (productInfo == null)
            {
                throw new ProductNotFoundException();
            }
            if (productInfo.Stock == 0)
            {
                throw new InsufficientProductInStockException();
            }
            if (productInfo.Stock < createOrderInput.Quantity)
            {
                throw new InsufficientProductInStockException();
            }
        }
    }
}
