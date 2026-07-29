using System.Collections.Generic;
using System.Linq;

class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double GetShippingCost()
    {
        return _customer.IsInUSA() ? 5.0 : 35.0;
    }

    public double GetTotalCost()
    {
        double productsTotal = _products.Sum(product => product.GetTotalCost());
        return productsTotal + GetShippingCost();
    }

    public List<Product> GetProducts()
    {
        return _products;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\size";

        foreach (Product product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\size";
        }

        return label.TrimEnd();
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\size{_customer.GetName()}\size{_customer.GetAddress().GetFullAddress()}";
    }
}