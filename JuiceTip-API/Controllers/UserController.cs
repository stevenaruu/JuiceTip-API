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
    [Route("user")]
    public class UserController : ControllerBase
    {
        private UserFacade userFacade;

        public UserController(UserFacade userFacade)
        {
            this.userFacade = userFacade;
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest user)
        {
            return await userFacade.Login(user);
        }

        [HttpPost("generate-otp")]
        [Produces("application/json")]
        public async Task<IActionResult> GenerateOTP([FromBody] OTP user)
        {
            return await userFacade.GenerateOtp(user);
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest user)
        {
            return await userFacade.RegisterUser(user);
        }

        [HttpPost("customer")]
        [Produces("application/json")]
        public async Task<IActionResult> Customer([FromBody] UserRequest user)
        {
            return await userFacade.Customer(user);
        }

        [HttpPost("topup")]
        [Produces("application/json")]
        public async Task<IActionResult> TopUp([FromBody] TopUpRequest user)
        {
            return await userFacade.TopUp(user);
        }

        [HttpPost("decrease-balance")]
        [Produces("application/json")]
        public async Task<IActionResult> DecreaseBalance([FromBody] TopUpRequest user)
        {
            return await userFacade.DecreaseBalance(user);
        }
    }
}
