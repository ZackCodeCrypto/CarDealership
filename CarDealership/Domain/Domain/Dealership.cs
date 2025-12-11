using CarDealership.Enums;
using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Dealership
{
    private string _name;
    private string _location;
    private HashSet<Car> _cars = [];
    private HashSet<Employment> _employments = [];

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
    public IReadOnlyCollection<Car> Cars => _cars.ToList().AsReadOnly();

    // Association class
    public IReadOnlyCollection<Employment> Employments => _employments.ToList().AsReadOnly();

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
    private readonly Dictionary<Guid, Car> _carsByVin = new();

    // Lookup by VIN
    public Car? GetCarByVin(Guid vin)
    {
        return _carsByVin.TryGetValue(vin, out var car) ? car : null;
    }

    // Add car using VIN as qualifier
    public void AddCar(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);

        if (_carsByVin.ContainsKey(car.VIN))
            throw new InvalidOperationException($"A car with VIN {car.VIN} is already stored in this dealership.");

        _carsByVin[car.VIN] = car;
        
        _cars.Add(car);
        
        car.AssignToDealership(this);
    }
    
    public void RemoveCarByVin(Guid vin)
    {
        if (!_carsByVin.TryGetValue(vin, out var car))
            throw new InvalidOperationException($"No car with VIN {vin} exists in this dealership.");

        _carsByVin.Remove(vin);
        _cars.Remove(car);
        
        car.AssignToDealership(null);
    }

    public void AddEmployment(Employment employment)
    {
        ArgumentNullException.ThrowIfNull(employment);

        var existingEmployment = _employments.FirstOrDefault(e => e.Employee == employment.Employee && e.IsActive);
        if (existingEmployment != null)
        {
            existingEmployment.EndDate = DateTime.Now;
        }

        _employments.Add(employment);
    }

    public void RemoveEmployment(Employment employment)
    {
        ArgumentNullException.ThrowIfNull(employment);
        _employments.Remove(employment);
    }
}

