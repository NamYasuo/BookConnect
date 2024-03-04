using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects.Models;
using DataAccess.Database;

namespace DataAccess.DAO
{
    public class InventoryDAO
    {
        public void AddInventoryItem(Inventory item)
        {
            using (var context = new AppDbContext())
            {
                context.Inventories.Add(item);
                context.SaveChanges();
            }
        }

        public void UpdateInventoryItem(Inventory item)
        {
            using (var context = new AppDbContext())
            {
                var existingItem = context.Inventories.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.ProductName = item.ProductName;
                    existingItem.ProductDescription = item.ProductDescription;
                    existingItem.QuantityAvailable = item.QuantityAvailable;
                    existingItem.Price = item.Price;
                    existingItem.SupplierId = item.SupplierId;
                    existingItem.DateAdded = item.DateAdded;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteInventoryItem(Guid itemId)
        {
            using (var context = new AppDbContext())
            {
                var item = context.Inventories.FirstOrDefault(i => i.ProductId == itemId);
                if (item != null)
                {
                    context.Inventor.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        public List<Inventory> GetInventoryItemsBySellerId(Guid sellerId)
        {
            using (var context = new AppDbContext())
            {
                return context.InventoryItems.Where(i => i.SupplierId == sellerId).ToList();
            }
        }
    }
}
