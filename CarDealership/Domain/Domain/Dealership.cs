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

    public Dealership(string name, string location)
    {
        Name = name;
        Location = location;
    }

    public void AddCar(Car car)
    {
        Cars.Add(car ?? throw new ArgumentNullException(nameof(car)));
    }
}
