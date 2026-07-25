using System;
using System.Collections.Generic;
class Program
{
static void Main(string[] args)
{
Console.WriteLine("===== ONLINE ORDERING SYSTEM ===== ");
// Create addresses
Address address1 = new("123 Main St", "Springfield", "IL", "USA");
Address address2 = new("456 Maple Ave", "Toronto", "ON", "Canada");
Address address3 = new("789 Oak Dr", "Austin", "TX", "USA");
// Create customers
Customer customer1 = new("John Smith", address1);
Customer customer2 = new("Maria Garcia", address2);
Customer customer3 = new("Robert Johnson", address3);
// Create products
Product product1 = new("Laptop", "P1001", 899.99m, 1);
Product product2 = new("Mouse", "P6", 24.99m, 2);
Product product3 = new("Keyboard", "P3", 79.99m, 1);
Product product4 = new("Monitor", "P4", 299.99m, 1);
Product product5 = new("USB Cable", "P1005", 12.99m, 3);
Product product6 = new("Headphones", "P1006", 149.99m, 6);
// Create Order 1 (USA customer)
Order order1 = new(customer1);
order1.AddProduct(product1);
order1.AddProduct(product2);
order1.AddProduct(product3);
// Create Order 2 (International customer)
Order order2 = new(customer2);
order2.AddProduct(product4);
order2.AddProduct(product5);
order2.AddProduct(product6);
// Create Order 3 (Another USA customer)
Order order3 = new(customer3);
order3.AddProduct(product1);
order3.AddProduct(product4);
order3.AddProduct(product5);
// Store orders in a list
List orders = new List { order1, order2, order3 };
// Display all orders
int orderNumber = 1;
foreach (Order order in orders)
{
Console.WriteLine($"ORDER #{orderNumber}");
Console.WriteLine(new string('-', 50));
Console.WriteLine("PACKING LABEL:");
Console.WriteLine(order.GetPackingLabel());
Console.WriteLine(" SHIPPING LABEL:");
Console.WriteLine(order.GetShippingLabel());
Console.WriteLine($" TOTAL PRICE: ${order.CalculateTotalCost():F2}");
Console.WriteLine(new string('=', 50) + " ");
orderNumber++;
}
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
}
}
