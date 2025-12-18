namespace CarDealership.Domain;

public interface IElectricEngine
{
    int BatterySize { get; }
    int MotorPower { get; }
}
