using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Mechanic : Employee
{
    private string _certification;

    public string Certification
    {
        get => _certification;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Certification cannot be empty.");
            _certification = value;
        }
    }

    public static ExtentRepository<Mechanic> Extent = new();

    public Mechanic(
        string name,
        string phoneNumber,
        string? email,
        DateTime startDate,
        decimal salary,
        string certification)
        : base(name, phoneNumber, startDate, salary, email)
    {
        Certification = certification;

        Extent.Add(this);
    }
}