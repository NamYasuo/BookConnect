using System;
using APIs.Services.Interfaces;
using BusinessObjects.Models.Ecom.Payment;
using DataAccess.DAO;

namespace APIs.Services.Payment
{
    public class TransactionService : ITransactionService
    {
        public int AddTransactionRecord(TransactionRecord transaction) => new TransactionDAO().AddTransactionRecord(transaction);

        public TransactionRecord? GetTransactionId(Guid refId) => new TransactionDAO().GetTransactionId(refId);

        public int IdentifyTransactor(Guid transId, Guid userId) => new TransactionDAO().IdentifyTransactor(transId, userId);
    }
}

