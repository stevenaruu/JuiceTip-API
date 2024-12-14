using JuiceTip_API.Data;
using JuiceTip_API.Facade;
using JuiceTip_API.Helper;
using JuiceTip_API.Model;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("rating")]
    public class RatingController : ControllerBase
    {
        private RatingFacade ratingFacade;
        public RatingController(RatingFacade ratingFacade)
        {
            this.ratingFacade = ratingFacade;
        }

        [HttpPost("user")]
        [Produces("application/json")]
        public async Task<IActionResult> UserRating([FromBody] UserRequest user)
        {
            return await ratingFacade.UserRating(user);
        }

        [HttpPost("insert")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertRating([FromBody] RatingRequest rating)
        {
            return await ratingFacade.InsertRating(rating);
        }
    }
}
