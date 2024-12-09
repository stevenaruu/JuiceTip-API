using JuiceTip_API.Data;
using JuiceTip_API.Factories;
using JuiceTip_API.Model;
using JuiceTip_API.Output;
using Microsoft.AspNetCore.Mvc;

namespace JuiceTip_API.Helper
{
    public class TransactionDetailHelper
    {
        private JuiceTipDBContext _dbContext;
        public TransactionDetailHelper(JuiceTipDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public StatusOutput InsertTransactionDetail([FromBody] TransactionDetail transactionDetail)
        {
            try
            {
                var returnValue = new StatusOutput();
                var data = _dbContext.TransactionDetail.Where(x => x.TransactionId == transactionDetail.TransactionId).FirstOrDefault();
                
                if (data != null)
                {
                    returnValue.statusCode = 201;
                    returnValue.message = "TransactionId already exist";

                    return returnValue;
                }

                var product = _dbContext.MsProduct.Where(x => x.ProductId == transactionDetail.ProductId).FirstOrDefault();

                if (product == null)
                {
                    returnValue.statusCode = 404;
                    returnValue.message = "Product not found";

                    return returnValue;
                }

                product.TransactionId = transactionDetail.TransactionId;
                
                _dbContext.MsProduct.Update(product);
                _dbContext.SaveChanges();

                var transactionFactory = new TransactionFactory();
                var newTransacionDetail = transactionFactory.CreateTransaction(transactionDetail);

                _dbContext.TransactionDetail.Add(newTransacionDetail);
                _dbContext.SaveChanges();

                returnValue.statusCode = 200;
                returnValue.message = "Success insert TransactionDetail";
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
