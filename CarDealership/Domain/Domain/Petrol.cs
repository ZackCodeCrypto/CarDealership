using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Petrol : Car
{
    private double _tankSize;
    private int _enginePower;

    public double TankSize
    {
        get => _tankSize;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Tank size must be positive.");
            _tankSize = value;
        }
    }
    public int EnginePower
    {
        get => _enginePower;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Engine power must be positive.");
            _enginePower = value;
        }
    }

    public static ExtentRepository<Petrol> Extent = new();

    public Petrol(string model, string make, int year, decimal price, UsageType usageType,
        double tankSize, int enginePower)
        : base(model, make, year, price, usageType)
    {
        TankSize = tankSize;
        EnginePower = enginePower;

        Extent.Add(this);
    }
}