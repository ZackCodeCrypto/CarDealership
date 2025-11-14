namespace CarDealership.Domain;

using System.Text.Json;

public class Car
{
    private string _model;
    private string _make;
    private int _year;
    private decimal _price;

    public string Model
    {
        get => _model;
        set => _model = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Model cannot be empty.", nameof(value))
            : value;
    }

    public string Make
    {
        get => _make;
        set => _make = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Make cannot be empty.", nameof(value))
            : value;
    }

    public int Year
    {
        get => _year;
        set
        {
            var currentYear = DateTime.Today.Year;
            if (value < 1980 || value > currentYear)
                throw new ArgumentOutOfRangeException(nameof(value), "Year must be reasonable and not in the future.");
            _year = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set => _price = value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.")
            : value;
    }

    public UsageType UsageType { get; set; }
    public CarStatus Status { get; private set; } = CarStatus.Available;

    // Multi-value: list of service records
    public IList<ServiceRecord> ServiceRecords { get; } = new List<ServiceRecord>();

    // Derived attribute: /mileage
    public double Mileage => ServiceRecords.Sum(sr => sr.MilesDriven);

    // Extent class
    private static readonly List<Car> _extent = new();
    public static IReadOnlyList<Car> Extent => _extent.AsReadOnly();

    public Car(string model, string make, int year, decimal price, UsageType usageType)
    {
        Model = model;
        Make = make;
        Year = year;
        Price = price;
        UsageType = usageType;

        _extent.Add(this);
    }

    public void AddServiceRecord(ServiceRecord record)
    {
        ServiceRecords.Add(record ?? throw new ArgumentNullException(nameof(record)));
    }

    public void MarkSold() => Status = CarStatus.Sold;

    // Extent persistence
    public static void SaveExtent(string filePath)
    {
        var json = JsonSerializer.Serialize(_extent);
        File.WriteAllText(filePath, json);
    }

    public static void LoadExtent(string filePath)
    {
        if (!File.Exists(filePath)) return;

        var json = File.ReadAllText(filePath);
        var loaded = JsonSerializer.Deserialize<List<Car>>(json);

        if (loaded == null) return;

        _extent.Clear();
        _extent.AddRange(loaded);
    }
}
