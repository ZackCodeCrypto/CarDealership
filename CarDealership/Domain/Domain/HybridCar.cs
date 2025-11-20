using CarDealership.Enums;
using CarDealership.Repositories;

namespace CarDealership.Domain;

public class HybridCar : Car
{
    public HybridType Type { get; set; }

    public static ExtentRepository<HybridCar> Extent = new();

    public HybridCar(string model, string make, int year, decimal price, UsageType usageType,
        HybridType type)
        : base(model, make, year, price, usageType)
    {
        Type = type;

        Extent.Add(this);
    }
}