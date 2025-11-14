namespace CarDealership;

public abstract class Person
{
    // Basic attribute
    private string _name;

    // Optional attribute
    private string? _email;

    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Name cannot be empty.", nameof(value))
            : value;
    }

    public string? Email
    {
        get => _email;
        set
        {
            if (value != null && string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty if provided.", nameof(value));

            _email = value;
        }
    }

    // Complex attribute
    public Address Address { get; private set; }

    protected Person(string name, Address address, string? email = null)
    {
        Name = name;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Email = email;
    }
}
