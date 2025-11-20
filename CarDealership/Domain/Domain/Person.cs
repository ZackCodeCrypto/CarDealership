using CarDealership.Repositories;

namespace CarDealership.Domain;

public abstract class Person
{
    // Basic Attribute
    private string _name;
    private string _phoneNumber;
    // Optional attribute
    private string? _email;  

    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Name cannot be empty.", nameof(value))
            : value;
    }
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be empty.", nameof(value));
            
            if (value.Length < 9)
                throw new ArgumentException("Phone number appears too short.", nameof(value));

            _phoneNumber = value;
        }
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

    protected Person(string name, string phoneNumber, string? email = null)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}