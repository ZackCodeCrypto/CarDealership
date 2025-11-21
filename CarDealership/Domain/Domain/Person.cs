using System.Text.RegularExpressions;

namespace CarDealership.Domain;

public abstract class Person
{
    // Basic Attribute
    private string _name;
    protected string _phoneNumber;
    // Optional attribute
    private string? _email;  
    private static Regex _phoneRegex = new(@"^\d{9}$");

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
            if (IsPhoneNumberCorrect(value))
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

    protected static bool IsPhoneNumberCorrect(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber));

        if (!_phoneRegex.IsMatch(phoneNumber))
            throw new ArgumentException("Phone number must be a 9-digit string", nameof(phoneNumber));

        return true;
    }
}