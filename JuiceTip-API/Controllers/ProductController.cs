using JuiceTip_API.Data;
using JuiceTip_API.Facade;
using JuiceTip_API.Helper;
using JuiceTip_API.Model;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using static JuiceTip_API.Output.ProductProgressOutput;

namespace JuiceTip_API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("product")]
    public class ProductController : Controller
    {
        private ProductFacade productFacade;
        public ProductController(ProductFacade productFacade)
        {
            this.productFacade = productFacade;
        }

        [HttpPost("upsert")]
        [Produces("application/json")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest product)
        {
            return await productFacade.AddProduct(product);
        }

        [HttpGet("")]
        [Produces("application/json")]
        public async Task<IActionResult> Products()
        {
            return await productFacade.Products();
        }

        [HttpPost("id")]
        [Produces("application/json")]
        public async Task<IActionResult> Product([FromBody] ProductByIdRequest product)
        {
            return await productFacade.Product(product);
        }

        [HttpPost("delete")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteProduct([FromBody] ProductByIdRequest product)
        {
            return await productFacade.DeleteProduct(product);
        }

        [HttpPost("progress")]
        [Produces("application/json")]
        public async Task<IActionResult> ProductProgress([FromBody] UserRequest user)
        {
            return await productFacade.ProductProgress(user);
        }
    }
}
