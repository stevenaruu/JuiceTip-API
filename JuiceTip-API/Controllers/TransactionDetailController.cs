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
    [Route("transaction-detail")]
    public class TransactionDetailController : ControllerBase
    {
        private TransactionDetailFacade transactionDetailFacade;
        public TransactionDetailController(TransactionDetailFacade transactionDetailFacade)
        {
            this.transactionDetailFacade = transactionDetailFacade;
        }

        [HttpPost("insert")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertTransactionDetail([FromBody] TransactionDetail transactionDetail)
        {
            try
            {
                return await transactionDetailFacade.InsertTransactionDetail(transactionDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
