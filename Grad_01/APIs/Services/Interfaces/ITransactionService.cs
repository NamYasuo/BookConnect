using System;
using BusinessObjects.Models.Ecom.Payment;

namespace APIs.Services.Interfaces
{
	public interface ITransactionService
	{
        public int AddTransactionRecord(TransactionRecord transaction);
        public TransactionRecord? GetTransactionId(Guid refId);
        int IdentifyTransactor(Guid transId, Guid userId);
    }
}

