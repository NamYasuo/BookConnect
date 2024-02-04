//using System;
//using BusinessObjects;
//using BusinessObjects.Models;

//namespace DataAccess.DAO
//{
//	public class TokenInfoDAO
//	{
//		public void AddTokenInfo(TokenInfo info)
//		{
//			try
//			{
//				using(var context = new AppDbContext())
//				{
//				     context.TokenInfos.AddAsync(info);
//					 context.SaveChangesAsync();
//				}

//			}catch(Exception e)
//			{
//				throw new Exception(e.Message);
//			}
//		}
//	}
//}

