using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Customer : Person
{
    private string _driversLicense;

    public string DriversLicense
    {
        get => _driversLicense;
        set => _driversLicense = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Driver's license cannot be empty.", nameof(value))
            : value;
    }

    public string DescriptionOfNeeds { get; set; }
    // Multi-valued attribute
    public List<string> ContactNumbers { get; } = new();
    
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
    }

    public void AddContactNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Contact number cannot be empty.", nameof(number));

        ContactNumbers.Add(number);

        Extent.Add(this);
    }
    
    // Association managing
    internal void AddTestDrive(TestDrive drive)
    {
        if (drive == null) throw new ArgumentNullException(nameof(drive));
        if (!_testDrives.Contains(drive))
        {
            _testDrives.Add(drive);
            Extent.Add(this);
        }
    }

    internal void RemoveTestDrive(TestDrive drive)
    {
        if (drive == null) throw new ArgumentNullException(nameof(drive));
        _testDrives.Remove(drive);
    }
}

