using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Customer customer1 = new Customer("Jane Doe", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Mouse", "WM-100", 15.99, 2));
        order1.AddProduct(new Product("USB-C Charger", "UC-200", 22.50, 1));
        order1.AddProduct(new Product("Keyboard", "KB-300", 34.75, 1));

        Address address2 = new Address("456 Maple Ave", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Miguel Santos", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Bluetooth Speaker", "BS-400", 49.99, 1));
        order2.AddProduct(new Product("Noise-Cancelling Headphones", "NH-500", 89.95, 1));

        DisplayOrder(order1);
        Console.WriteLine();
        DisplayOrder(order2);
    }

    static void DisplayOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine();
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine();
        Console.WriteLine($"Total Price: ${order.GetTotalCost():0.00}");
    }
}