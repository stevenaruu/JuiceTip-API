using JuiceTip_API.Data;
using JuiceTip_API.Helper;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Facade
{
    public class RatingFacade
    {
        private RatingHelper ratingHelper;
        public RatingFacade(RatingHelper ratingHelper)
        {
            this.ratingHelper = ratingHelper;
        }
        public async Task<IActionResult> UserRating([FromBody] UserRequest user)
        {
            try
            {
                var objJSON = new ReviewOutput()
                {
                    payload = ratingHelper.UserRating(user)
                };
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        public async Task<IActionResult> InsertRating([FromBody] RatingRequest rating)
        {
            try
            {
                var objJSON = new StatusOutput();
                objJSON = ratingHelper.InsertRating(rating);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
