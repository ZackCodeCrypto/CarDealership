using CarDealership.Repositories;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public Car? Car { get; private set; }

    public string AccessoryType { get; set; }

    public static ExtentRepository<Accessory> Extent = new();

    public Accessory(string name, string accessoryType, decimal price)
    {
        Name = name;
        AccessoryType = accessoryType;
        Price = price;

        Extent.Add(this);
    }

    public void AssignToCar(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);
        
        if (Car != null)
            throw new InvalidOperationException("Accessory is already assigned to a car.");

        Car = car;
        Car.AddAccessory(this);
    }

    public void RemoveFromCar()
    {
        if (Car == null)
            return;

        var previousCar = Car;
        Car = null;
        previousCar.RemoveAccessory(this);
    }
}
