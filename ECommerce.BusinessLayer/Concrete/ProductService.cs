using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Dto.Input;
using ECommerce.BusinessLayer.Dto.Output;
using ECommerce.BusinessLayer.Exceptions;
using ECommerce.Data.Abstract;
using ECommerce.Data.Entity;
using ECommerce.Data.Request;
using System.Net;

namespace ECommerce.BusinessLayer.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICampaignService _campaignService;
        public ProductService(IProductRepository productRepository, ICampaignService campaignService)
        {
            _productRepository = productRepository;
            _campaignService = campaignService;
        }
        public void CreateProduct(CreateProductInput createProductInput)
        {
            CreateProductInputValidate(createProductInput);
            _productRepository.CreateProduct(new Product(createProductInput.Code, createProductInput.Price, createProductInput.Stock));
        }
        public GetProductInfoOutput GetProductInfo(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                throw new ValidationException(nameof(productCode));
            }

            var product = _productRepository.GetProduct(productCode);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            return ParseGetProductInfoResponse(product);
        }
        private GetProductInfoOutput ParseGetProductInfoResponse(Product product)
        {
            var getProductInfoOutput = new GetProductInfoOutput();
            var campaignInfo = _campaignService.GetCampaignInfoByProductCode(product.Code);

            if (campaignInfo != null && campaignInfo.Status == (int)Dto.CampaignStatusEnum.Active)
            {
                getProductInfoOutput.Price = product.Price + product.Discount;
                getProductInfoOutput.CampaignId = campaignInfo.Id;
            }
            else
                getProductInfoOutput.Price = product.Price;

            getProductInfoOutput.Stock = product.Stock;

            return getProductInfoOutput;
        }

        private void CreateProductInputValidate(CreateProductInput createProductInput)
        {
            if (string.IsNullOrWhiteSpace(createProductInput.Code))
            {
                throw new ValidationException(nameof(createProductInput.Code));
            }
            if (_productRepository.GetProduct(createProductInput.Code) != null)
            {
                throw new BaseServiceException("This ProductCode has been used before", (int)(HttpStatusCode.BadRequest));
            }
            if (createProductInput.Price <= 0)
            {
                throw new InvalidPriceException();
            }

            if (createProductInput.Stock <= 0)
            {
                throw new InvalidStockException();
            }
        }
        public void UpdateProductStock(UpdateProductStockInput updateProductStockInput)
        {
            if (updateProductStockInput.Quantity <= 0)
            {
                throw new InvalidQuantityException();
            }
            var product = _productRepository.GetProduct(updateProductStockInput.ProductCode);
            if (product == null)
                throw new ProductNotFoundException();

            _productRepository.UpdateProductStock(new UpdateProductStockRequest(updateProductStockInput.ProductCode, product.Stock - updateProductStockInput.Quantity));
        }
        public void UpdateProductDiscount(string productCode, decimal discount)
        {
            var product = _productRepository.GetProduct(productCode);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            _productRepository.UpdateProductDiscount(new UpdateProductDiscountRequest(productCode, discount));
        }
    }
}
