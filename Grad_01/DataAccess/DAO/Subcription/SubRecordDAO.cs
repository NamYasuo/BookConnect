using System;
using BusinessObjects;
using BusinessObjects.DTO.Subscription;
using BusinessObjects.Models.Creative;

namespace DataAccess.DAO.Subcription
{
	public class SubRecordDAO
	{
		public int AddNewSubRecord(SubRecord record)
		{
			try
			{
				using (var context = new AppDbContext())
				{
					context.SubRecords.Add(record);
					return context.SaveChanges();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
	//public IEnumerable<SubRecordDetailsDTO> GetSubRecordDetailsBySubcriberId(Guid subcriberId)
	//{
	//	try
	//	{
	//		List<SubRecordDetailsDTO> result = new List<SubRecordDetailsDTO>();
	//		using (var context = new AppDbContext())
	//		{
	//			result = context.SubRecords.Where(s => s.S)
	//		}
	//	}
	//	catch(Exception e)
	//	{
	//		throw new Exception(e.Message);
	//	}
	//}
	    
}

