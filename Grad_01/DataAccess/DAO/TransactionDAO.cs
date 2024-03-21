using System;
using System.Transactions;
using BusinessObjects;
using BusinessObjects.Models.Ecom.Payment;

namespace DataAccess.DAO
{
	public class TransactionDAO
	{
		public int AddTransactionRecord(TransactionRecord transaction)
		{
			try
			{
				using (var context = new AppDbContext())
				{
					context.Transactions.Add(transaction);
					return context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TransactionRecord? GetTransactionId(Guid refId)
		{
			try
			{
				TransactionRecord? result = null;
				using (var context = new AppDbContext())
				{
					result = context.Transactions.Where(t => t.TransactionId == refId).FirstOrDefault();
				}
				return result;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public int IdentifyTransactor(Guid transId, Guid userId)
		{
			try
			{
				using(var context = new AppDbContext())
				{
					TransactionRecord? record = context.Transactions.Where(t => t.TransactionId == transId).SingleOrDefault();
					if(record != null)
					{
						record.UserId = userId;
					}
					return context.SaveChanges();
				}
			}
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}

