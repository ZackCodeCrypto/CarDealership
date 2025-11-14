namespace CarDealership.Domain;

public abstract class Employee : Person
{
    private DateTime _startDate;
    private DateTime? _endDate;
    private decimal _salary;

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            if (value > DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(value), "Start date cannot be in the future.");
            _startDate = value;
        }
    }

    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            if (value != null && value < StartDate)
                throw new ArgumentOutOfRangeException(nameof(value), "End date cannot be before start date.");
            _endDate = value;
        }
    }

    public decimal Salary
    {
        get => _salary;
        set => _salary = value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "Salary cannot be negative.")
            : value;
    }

    protected Employee(
        string name,
        Address address,
        DateTime startDate,
        decimal salary,
        string? email = null)
        : base(name, address, email)
    {
        StartDate = startDate;
        Salary = salary;
    }
}
