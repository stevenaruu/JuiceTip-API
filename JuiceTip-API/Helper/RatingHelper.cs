using JuiceTip_API.Data;
using JuiceTip_API.Factories;
using JuiceTip_API.Model;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;
using static JuiceTip_API.Output.ReviewOutput;

namespace JuiceTip_API.Helper
{
    public class RatingHelper
    {
        private JuiceTipDBContext _dbContext;
        public RatingHelper(JuiceTipDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public StatusOutput InsertRating([FromBody] RatingRequest rating)
        {
            try
            {
                var returnValue = new StatusOutput();
                rating.RatingId = _dbContext.MsRating
                    .Where(x => x.Rating == rating.Rating)
                    .Select(x => x.RatingId)
                    .FirstOrDefault();

                var ratingFactory = new RatingFactory();
                var newRating = ratingFactory.CreateRating(rating);

                _dbContext.TrReview.Add(newRating);
                _dbContext.SaveChanges();

                returnValue.statusCode = 200;
                returnValue.message = "Success insert review";
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ReviewModel> UserRating([FromBody] UserRequest user)
        {
            try
            {
                var data = (from usr in _dbContext.TrReview.Where(x => x.UserId == user.UserId)

                            join pep in _dbContext.MsUser
                            on usr.CustomerId equals pep.UserId

                            join rating in _dbContext.MsRating
                            on usr.RatingId equals rating.RatingId

                            select new ReviewModel
                            {
                                CustomerName = pep.FirstName + " " + pep.LastName,
                                Rating = rating.Rating,
                                Comment = usr.Comment,
                                ReviewDate = usr.ReviewDate
                            }).ToList();
                    
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
