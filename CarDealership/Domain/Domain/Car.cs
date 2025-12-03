using CarDealership.Enums;

namespace CarDealership.Domain;

public abstract class Car
{
    private string _model;
    private string _make;
    private int _year;
    private decimal _price;

    public string VIN { get; internal set; } = Guid.NewGuid().ToString();

    public string Model
    {
        get => _model;
        private set => _model = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Model cannot be empty.", nameof(value))
            : value;
    }
    public string Make
    {
        get => _make;
        private set => _make = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Make cannot be empty.", nameof(value))
            : value;
    }
    public int Year
    {
        get => _year;
        private set
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
    }

    public void AddServiceRecord(ServiceRecord record)
    {
        ServiceRecords.Add(record ?? throw new ArgumentNullException(nameof(record)));
    }

    public void MarkSold() => Status = CarStatus.Sold;
    
    // Back-reference to dealership
    public Dealership? Dealership { get; private set; }

   // Internal method for association
   internal void AssignToDealership(Dealership? dealership)
   {
       if (dealership == null)
       {
           Dealership = null;
           return;
       }

       if (Dealership != null && Dealership != dealership)
           throw new InvalidOperationException("This car is already assigned to a different dealership.");

       Dealership = dealership;
   }


}


