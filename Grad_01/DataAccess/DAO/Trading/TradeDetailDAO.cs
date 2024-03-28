/*using BusinessObjects.DTO;
using BusinessObjects.Models.Creative;
using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models.Trading;

namespace DataAccess.DAO.Trading
{
    public class TradeDetailDAO
    {
        //Get tradeDetail by id
        public TradeDetail GetTradeDetailById(Guid tradeDetailId)
        {
            TradeDetail? tradeDetail = new TradeDetail();
            try
            {
                using (var context = new AppDbContext())
                {
                    tradeDetail = context.TradeDetails.Where(b => b.TradeDetailId == tradeDetailId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (tradeDetail != null) return tradeDetail;
            else throw new NullReferenceException();
        }

        //Delete TradeDetail by id
        public int DeleteTradeDetailById(Guid tradeDetailId)
        {
            try
            {
                int result = 0;
                using (var context = new AppDbContext())
                {
                    TradeDetail tradeDetail = GetTradeDetailById(tradeDetailId);
                    if (tradeDetail != null)
                    {
                        context.TradeDetails.Remove(tradeDetail);
                        result = context.SaveChanges();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
*/