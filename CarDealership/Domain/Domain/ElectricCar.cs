using CarDealership.Enums;
using CarDealership.Repositories;

namespace CarDealership.Domain;

public class ElectricCar : Car, IElectricEngine
{
    private int _batterySize;
    private int _motorPower;

    public int BatterySize
    {
        get => _batterySize;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Battery size must be positive.");
            _batterySize = value;
        }
    }
    public int MotorPower
    {
        get => _motorPower;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Motor power must be positive.");
            _motorPower = value;
        }
    }

    public static ExtentRepository<ElectricCar> Extent = new();

    public ElectricCar(string model, string make, int year, decimal price, UsageType usageType,
        int batterySize, int motorPower)
        : base(model, make, year, price, usageType)
    {
        BatterySize = batterySize;
        MotorPower = motorPower;

        Extent.Add(this);
    }
}

