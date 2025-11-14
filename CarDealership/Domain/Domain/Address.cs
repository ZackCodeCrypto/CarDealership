namespace CarDealership.Domain;

public class Address
{
    private string _street;
    private string _city;

    public string Street
    {
        get => _street;
        set => _street = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Street cannot be empty.", nameof(value))
            : value;
    }

    public string City
    {
        get => _city;
        set => _city = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("City cannot be empty.", nameof(value))
            : value;
    }

    public string Country { get; set; } = "United States";

    public Address(string street, string city, string country = "United States")
    {
        Street = street;
        City = city;
        Country = country;
    }
}
