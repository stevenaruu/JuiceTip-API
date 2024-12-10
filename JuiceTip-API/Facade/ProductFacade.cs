using JuiceTip_API.Data;
using JuiceTip_API.Helper;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Facade
{
    public class ProductFacade
    {
        private ProductHelper productHelper;
        public ProductFacade(ProductHelper productHelper)
        {
            this.productHelper = productHelper;
        }

        public async Task<IActionResult> AddProduct(ProductRequest product)
        {
            try
            {
                var objJSON = new ProductOutput
                {
                    payload = productHelper.UpsertProduct(product)
                };
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> Products()
        {
            try
            {
                var objJSON = new AllProductOutput
                {
                    payload = productHelper.GetProducts()
                };
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> Product(ProductByIdRequest product)
        {
            try
            {
                var objJSON = new ProductByIdOutput
                {
                    payload = productHelper.GetProductById(product)
                };
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteProduct(ProductByIdRequest product)
        {
            try
            {
                var objJSON = productHelper.DeleteProductById(product.ProductId);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> ProductProgress(UserRequest user)
        {
            try
            {
                var objJSON = new ProductProgressOutput
                {
                    payload = productHelper.GetAllProductProgress(user)
                };
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}