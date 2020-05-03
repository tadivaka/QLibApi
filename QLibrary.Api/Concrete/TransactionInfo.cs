using System;
namespace QLibrary.Api.Concrete
{
    /// <summary>
    /// TransactionInfo
    /// </summary>
    public class TransactionInfo
    {
        public System.Net.HttpStatusCode status { get; set; }
        public MessageCode msgCode { get; set; }
        public string message { get; set; }
        public Object transactionObject { get; set; }
    }

    public enum MessageCode
    {
        Void = 0,
        Created = 1,
        Updated = 2,
        Deleted = 3,
        Failed = 4
    }
}
