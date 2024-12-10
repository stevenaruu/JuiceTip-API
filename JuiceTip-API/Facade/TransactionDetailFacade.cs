using JuiceTip_API.Helper;
using JuiceTip_API.Model;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Facade
{
    public class TransactionDetailFacade
    {
        private TransactionDetailHelper transactionDetailHelper;
        public TransactionDetailFacade(TransactionDetailHelper transactionDetailHelper)
        {
            this.transactionDetailHelper = transactionDetailHelper;
        }
        public async Task<IActionResult> InsertTransactionDetail(TransactionDetail transactionDetail)
        {
            try
            {
                var objJSON = new StatusOutput();
                objJSON = transactionDetailHelper.InsertTransactionDetail(transactionDetail);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
