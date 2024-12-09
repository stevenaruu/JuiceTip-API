using JuiceTip_API.Data;
using JuiceTip_API.Model;

namespace JuiceTip_API.Factories
{
    public class RatingFactory
    {
        public TrReview CreateRating(RatingRequest rating)
        {
            return new TrReview
            {
                Comment = rating.Comment,
                RatingId = rating.RatingId,
                CustomerId = rating.CustomerId,
                UserId = rating.UserId,
                ReviewDate = DateTime.Now
            };
        }
    }
}
