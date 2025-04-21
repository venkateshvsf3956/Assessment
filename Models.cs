using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
namespace SalesAnalyticsConsole.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Region Region { get; set; }
        public DateTime DateOfSale { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal ShippingCost { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int QuantitySold { get; set; }
        public decimal Discount { get; set; }
    }

    public class SalesCsvRow
    {
            [Name("Order ID")]
            public string OrderId { get; set; }

            [Name("Product ID")]
            public string ProductId { get; set; }

            [Name("Customer ID")]
            public string CustomerId { get; set; }

            [Name("Product Name")]
            public string ProductName { get; set; }

            [Name("Category")]
            public string Category { get; set; }

            [Name("Region")]
            public string Region { get; set; }

            [Name("Date of Sale")]
            public DateTime DateOfSale { get; set; }

            [Name("Quantity Sold")]
            public int QuantitySold { get; set; }

            [Name("Unit Price")]
            public decimal UnitPrice { get; set; }

            [Name("Discount")]
            public decimal Discount { get; set; }

            [Name("Shipping Cost")]
            public decimal ShippingCost { get; set; }

            [Name("Payment Method")]
            public string PaymentMethod { get; set; }

            [Name("Customer Name")]
            public string CustomerName { get; set; }

            [Name("Customer Email")]
            public string CustomerEmail { get; set; }

            [Name("Customer Address")]
            public string CustomerAddress { get; set; }

        }
    }
