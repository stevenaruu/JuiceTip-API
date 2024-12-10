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
    [Route("region")]
    public class RegionController : ControllerBase
    {
        private RegionFacade regionFacade;
        public RegionController(RegionFacade regionFacade)
        {
            this.regionFacade = regionFacade;
        }

        [HttpPost("")]
        [Produces("application/json")]
        public async Task<IActionResult> Region([FromBody] RegionRequest region)
        {
            try
            {
                return await regionFacade.Region(region);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
