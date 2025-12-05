using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Customer : Person
{
    private string _driversLicense;
    private HashSet<string> _contactNumbers = [];

    public string DriversLicense
    {
        get => _driversLicense;
        set => _driversLicense = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Driver's license cannot be empty.", nameof(value))
            : value;
    }

    public string DescriptionOfNeeds { get; set; }
    // Multi-valued attribute
    public IReadOnlyCollection<string> ContactNumbers => _contactNumbers.ToList().AsReadOnly();

    // Basic association Customer to TestDrives (0..*)
    private readonly List<TestDrive> _testDrives = new();
    public IReadOnlyCollection<TestDrive> TestDrives => _testDrives.AsReadOnly();

    public static ExtentRepository<Customer> Extent = new();

    public Customer(
        string name,
        string phoneNumber,
        string driversLicense,
        string descriptionOfNeeds,
        string? email = null)
        : base(name, phoneNumber, email)
    {
        DriversLicense = driversLicense;
        DescriptionOfNeeds = descriptionOfNeeds;

        Extent.Add(this);
    }

    public void AddContactNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Contact number cannot be empty.", nameof(number));

        _contactNumbers.Add(number);
    }
    
    // Association managing
    internal void AddTestDrive(TestDrive drive)
    {
        ArgumentNullException.ThrowIfNull(drive);

        if (!_testDrives.Contains(drive))
        {
            _testDrives.Add(drive);
        }
    }

    internal void RemoveTestDrive(TestDrive drive)
    {
        ArgumentNullException.ThrowIfNull(drive);

        _testDrives.Remove(drive);
    }
}

