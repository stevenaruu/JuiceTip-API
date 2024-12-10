using JuiceTip_API.Data;
using JuiceTip_API.Facade;
using JuiceTip_API.Helper;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private CategoryFacade categoryFacade;

        public CategoryController(CategoryFacade categoryFacade)
        {
            this.categoryFacade = categoryFacade;
        }

        [HttpPost("")]
        [Produces("application/json")]
        public async Task<IActionResult> Category([FromBody] CategoryRequest category)
        {
            return await categoryFacade.Category(category);
        }
    }
}
