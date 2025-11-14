namespace CarDealership;

public class Customer : Person
{
    private string _driversLicense;

    // Multi-value attribute
    public List<string> ContactNumbers { get; } = new();

    public string DriversLicense
    {
        get => _driversLicense;
        set => _driversLicense = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Driver's license cannot be empty.", nameof(value))
            : value;
    }

    public string DescriptionOfNeeds { get; set; }

    public Customer(
        string name,
        Address address,
        string driversLicense,
        string descriptionOfNeeds,
        string? email = null)
        : base(name, address, email)
    {
        DriversLicense = driversLicense;
        DescriptionOfNeeds = descriptionOfNeeds;
    }

    public void AddContactNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Contact number cannot be empty.", nameof(number));

        ContactNumbers.Add(number);
    }
}
