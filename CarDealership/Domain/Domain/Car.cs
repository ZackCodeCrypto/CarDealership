namespace CarDealership.Domain;

using CarDealership.Repositories;


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
    public IList<ServiceRecord> ServiceRecords { get; } = new List<ServiceRecord>();
    public double Mileage => ServiceRecords.Sum(sr => sr.MilesDriven);

    public Car(string model, string make, int year, decimal price, UsageType usageType)
    {
        Model = model;
        Make = make;
        Year = year;
        Price = price;
        UsageType = usageType;

        _repository.Add(this);
    }

    public void AddServiceRecord(ServiceRecord record)
    {
        ServiceRecords.Add(record ?? throw new ArgumentNullException(nameof(record)));
    }

    public void MarkSold() => Status = CarStatus.Sold;

    #region Extent
    private static ExtentRepository<Car> _repository = new("extents/Car.json");
    public static IReadOnlyList<Car> Extent => _repository.Extent;
    public static void SaveExtent() => _repository.SaveExtent();
    public static void LoadExtent() => _repository.LoadExtent();
    public static void DeleteExtent() => _repository.DeleteExtent();
    #endregion
}
