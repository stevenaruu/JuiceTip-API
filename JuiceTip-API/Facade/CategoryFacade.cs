using JuiceTip_API.Data;
using JuiceTip_API.Helper;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Facade
{
    public class CategoryFacade
    {
        private CategoryHelper categoryHelper;
        public CategoryFacade(CategoryHelper categoryHelper)
        {
            this.categoryHelper = categoryHelper;
        }
        public async Task<IActionResult> Category(CategoryRequest category)
        {
            try
            {
                var objJSON = new CategoryOutput()
                {
                    payload = categoryHelper.GetCategory(category)
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
