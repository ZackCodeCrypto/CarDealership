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

    public Manager(
        string name,
        string phoneNumber,
        string? email,
        DateTime startDate,
        decimal salary,
        string department)
        : base(name, phoneNumber, startDate, salary, email)
    {
        Department = department;
    }
}