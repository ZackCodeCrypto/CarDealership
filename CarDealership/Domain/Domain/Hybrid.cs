using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Hybrid : Car
{
    private string _type;

    public string Type
    {
        get => _type;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Hybrid type cannot be empty.");
            _type = value;
        }
    }

    public static ExtentRepository<Hybrid> Extent = new();

    public Hybrid(string model, string make, int year, decimal price, UsageType usageType,
        string type)
        : base(model, make, year, price, usageType)
    {
        Type = type;

        Extent.Add(this);
    }
}