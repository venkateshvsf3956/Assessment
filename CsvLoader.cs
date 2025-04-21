using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using SalesAnalyticsConsole.Data;
using SalesAnalyticsConsole.Models;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace SalesAnalyticsConsole.Services
{
    public class CsvLoader
    {
        private readonly AppDbContext _db;

        public CsvLoader(AppDbContext db) => _db = db;

        public async Task LoadFromCsvAsync(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var rows = csv.GetRecords<SalesCsvRow>();

            foreach (var row in rows)
            {
                var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Email == row.CustomerEmail)
                    ?? new Customer { Name = row.CustomerName, Email = row.CustomerEmail, Address = row.CustomerAddress };

                var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name == row.Category)
                    ?? new Category { Name = row.Category };

                var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == row.ProductName)
                    ?? new Product { Name = row.ProductName, Category = category, UnitPrice = row.UnitPrice };

                var region = await _db.Regions.FirstOrDefaultAsync(r => r.Name == row.Region)
                    ?? new Region { Name = row.Region };

                var order = new Order
                {
                    DateOfSale = row.DateOfSale,
                    Customer = customer,
                    Region = region,
                    PaymentMethod = row.PaymentMethod,
                    ShippingCost = row.ShippingCost,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Product = product, QuantitySold = row.QuantitySold, Discount = row.Discount }
                    }
                };

                _db.Orders.Add(order);
            }

            await _db.SaveChangesAsync();
        }
    }
}
