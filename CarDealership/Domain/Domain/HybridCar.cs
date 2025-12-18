using CarDealership.Enums;
using CarDealership.Repositories;

namespace CarDealership.Domain;

public class HybridCar : PetrolCar, IElectricEngine
{
    private int _batterySize;
    private int _motorPower;

    public int BatterySize
    {
        get => _batterySize;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _batterySize = value;
        }
    }

    public int MotorPower
    {
        get => _motorPower;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            _motorPower = value;
        }
    }
    
    public HybridType Type { get; set; }

    public static ExtentRepository<HybridCar> Extent = new();

    public HybridCar(
        string model,
        string make,
        int year,
        decimal price,
        UsageType usageType,
        double tankSize,
        int enginePower,
        int batterySize,
        int motorPower, HybridType type)
        : base(model, make, year, price, usageType, tankSize, enginePower)
    {
        BatterySize = batterySize;
        MotorPower = motorPower;
        
        Type = type;

        Extent.Add(this);
    }
}
