using JuiceTip_API.Data;
using JuiceTip_API.Helper;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Facade
{
    public class UserFacade
    {
        private UserHelper userHelper;

        public UserFacade(UserHelper userHelper)
        {
            this.userHelper = userHelper;
        }

        public async Task<IActionResult> Login(LoginRequest user)
        {
            try
            {
                var objJSON = new UserOutput { payload = userHelper.GetUser(user) };
                return objJSON.payload != null ? new OkObjectResult(objJSON) : new BadRequestObjectResult("Wrong Credential.");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> GenerateOtp(OTP user)
        {
            try
            {
                var otp = userHelper.SendOTPEmail(user);
                OTP.Otp = otp;
                return new OkObjectResult("Success Send OTP");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> RegisterUser(RegisterRequest user)
        {
            try
            {
                if (OTP.Otp == user.Otp && userHelper.CheckDuplicateEmail(user) == null)
                {
                    var objJSON = new UserOutput { payload = userHelper.InsertUser(user) };
                    return new OkObjectResult(objJSON);
                }
                return new BadRequestObjectResult("Invalid OTP or email already in use");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> Customer(UserRequest user)
        {
            try
            {
                var objJSON = new CustomerOutput { payload = userHelper.GetUserById(user) };
                return objJSON.payload != null ? new OkObjectResult(objJSON) : new BadRequestObjectResult("Wrong UserId.");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> TopUp(TopUpRequest user)
        {
            try
            {
                var objJSON = new UserOutput { payload = userHelper.TopUp(user) };
                return objJSON.payload != null ? new OkObjectResult(objJSON) : new BadRequestObjectResult("Wrong UserId.");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> DecreaseBalance(TopUpRequest user)
        {
            try
            {
                var objJSON = new UserOutput { payload = userHelper.DecreaseBalance(user) };
                return objJSON.payload != null ? new OkObjectResult(objJSON) : new BadRequestObjectResult("Wrong UserId.");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
