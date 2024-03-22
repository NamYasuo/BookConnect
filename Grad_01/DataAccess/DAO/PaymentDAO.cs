using System;
using BusinessObjects;
using BusinessObjects.DTO;
using BusinessObjects.Models.Ecom.Payment;

namespace DataAccess.DAO
{
	public class PaymentDAO
	{
		//Add new payment details
		//public Guid AddNewPaymentDetails(PaymentDetailsDTO data)
		//{
  //          try
		//	{
  //              PaymentDetails details = new PaymentDetails()
  //              {
  //                  PaymentId = Guid.NewGuid(),
  //                  Content = data.Content,
  //                  Currency = data.Currency,
  //                  RequiredAmount = data.RequiredAmount,
  //                  PaidDate = data.PaidDate,
  //                  Language = data.Language,
  //                  PaymentGate = data.PaymentGate,
  //                  Status = data.Status,
  //                  LastMessage = data.LastMessage
  //              };
  //              using (var context = new AppDbContext())
		//		{
  //                  context.PaymentDetails.Add(details);

		//			int result = context.SaveChanges();
		//			if (result > 0) return details.PaymentId;
		//			else return Guid.Empty;
		//		}
		//	}
		//	catch(Exception e)
		//	{
		//		throw new Exception(e.Message);
		//	}
		//}
	}
}

