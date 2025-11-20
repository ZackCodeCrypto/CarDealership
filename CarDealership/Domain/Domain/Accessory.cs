using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Accessory
{
    private string _name;
    private decimal _price;

    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Name cannot be empty.", nameof(value))
            : value;
    }
    public decimal Price
    {
        get => _price;
        set => _price = value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.")
            : value;
    }

    public string AccessoryType { get; set; }

    public static ExtentRepository<Accessory> Extent = new();

    public Accessory(string name, string accessoryType, decimal price)
    {
        Name = name;
        AccessoryType = accessoryType;
        Price = price;

        Extent.Add(this);
    }
}
