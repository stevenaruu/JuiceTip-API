using JuiceTip_API.Model;

namespace JuiceTip_API.Factories
{
    public class TransactionFactory
    {
        public TransactionDetail CreateTransaction(TransactionDetail transactionDetail)
        {
            return new TransactionDetail
            {
                TransactionId = transactionDetail.TransactionId,
                ApplicationFee = transactionDetail.ApplicationFee,
                JustiperId = transactionDetail.JustiperId,
                ProductId = transactionDetail.ProductId,
                SubtotalPayment = transactionDetail.SubtotalPayment,
                SubtotalProduct = transactionDetail.SubtotalProduct,
                TransactionStatus = transactionDetail.TransactionStatus
            };
        }
    }
}
