using JuiceTip_API.Data;
using JuiceTip_API.Helper;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Facade
{
    public class RegionFacade
    {
        private RegionHelper regionHelper;
        public RegionFacade(RegionHelper regionHelper)
        {
            this.regionHelper = regionHelper;
        }
        public async Task<IActionResult> Region(RegionRequest region)
        {
            try
            {
                var objJSON = new RegionOutput()
                {
                    payload = regionHelper.GetRegion(region)
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
