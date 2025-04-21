using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SalesAnalyticsConsole.Data;

namespace SalesAnalyticsConsole.Services
{
    public class RevenueCalculator
    {
        private readonly AppDbContext _db;

        public RevenueCalculator(AppDbContext db) => _db = db;

        public async Task<decimal> GetTotalRevenueAsync(DateTime start, DateTime end)
        {
            var orders = await _db.Orders
                .Where(o => o.DateOfSale >= start && o.DateOfSale <= end)
                .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                .ToListAsync();

            decimal total = 0;

            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems)
                {
                    var revenue = (item.QuantitySold * item.Product.UnitPrice) - item.Discount;
                    total += revenue;
                }

                total += order.ShippingCost;
            }

            return total;
        }
    }
}
