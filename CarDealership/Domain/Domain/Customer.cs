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
    }
}