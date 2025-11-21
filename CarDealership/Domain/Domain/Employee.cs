namespace CarDealership.Domain;

public abstract class Employee : Person
{
    private DateTime _startDate;
    private DateTime? _endDate;
    private decimal _salary;
    private List<string> _contactNumbers;

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

    public IReadOnlyList<string> ContactNumbers
    {
        get => _contactNumbers.AsReadOnly();
    }

    protected Employee(
        string name,
        string phoneNumber,
        DateTime startDate,
        decimal salary,
        string? email = null)
        : base(name, phoneNumber, email)
    {
        StartDate = startDate;
        Salary = salary;

        _contactNumbers = [phoneNumber];
    }

    public void AddContactNumber(string contactNumber)
    {
        if (IsPhoneNumberCorrect(contactNumber))
        {
            _contactNumbers.Add(contactNumber);
        }
    }

    public void RemoveContactNumber(string contactNumber)
    {
        if (_contactNumbers.Count == 1 && _contactNumbers.Contains(contactNumber))
        {
            throw new InvalidOperationException("Cannot remove the last contact number.");
        }
        _contactNumbers.Remove(contactNumber);
    }
}
