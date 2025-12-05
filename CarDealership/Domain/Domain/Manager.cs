using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Manager : Employee
{
    private string _department;

    public string Department
    {
        get => _department;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Department cannot be empty.");
            _department = value;
        }
    }

    public static ExtentRepository<Manager> Extent = new();

    public Manager(
        string name,
        string phoneNumber,
        string? email,
        string department)
        : base(name, phoneNumber, email)
    {
        Department = department;

        Extent.Add(this);
    }
}