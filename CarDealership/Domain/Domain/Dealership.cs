using CarDealership.Enums;
using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Dealership
{
    private string _name;
    private string _location;

    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Name cannot be empty.", nameof(value))
            : value;
    }
    public string Location
    {
        get => _location;
        set => _location = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Location cannot be empty.", nameof(value))
            : value;
    }

    // Multi-value association
    public IList<Car> Cars { get; } = new List<Car>();

    // Derived attribute: /carsAvailable
    public int CarsAvailable => Cars.Count(c => c.Status == CarStatus.Available);

    public static ExtentRepository<Dealership> Extent = new();

    public Dealership(string name, string location)
    {
        Name = name;
        Location = location;

        Extent.Add(this);
    }
    
    // Qualified association structure
    private readonly Dictionary<string, Car> _carsByVin = new();

   // Lookup by VIN
    public Car? GetCarByVin(string vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            throw new ArgumentException("VIN cannot be empty.", nameof(vin));

        return _carsByVin.TryGetValue(vin, out var car) ? car : null;
    }

   // Add car using VIN as qualifier
    public void AddCarByVin(Car car)
    {
        if (car == null)
            throw new ArgumentNullException(nameof(car));

        if (_carsByVin.ContainsKey(car.VIN))
            throw new InvalidOperationException($"A car with VIN {car.VIN} is already stored in this dealership.");

        _carsByVin[car.VIN] = car;
        
        Cars.Add(car);
        
        car.AssignToDealership(this);
    }
    
    public void RemoveCarByVin(string vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            throw new ArgumentException("VIN cannot be empty.", nameof(vin));

        if (!_carsByVin.TryGetValue(vin, out var car))
            throw new InvalidOperationException($"No car with VIN {vin} exists in this dealership.");

        _carsByVin.Remove(vin);
        Cars.Remove(car);
        
        car.AssignToDealership(null);
    }

}

