namespace CarDealership;

public class Salesman : Employee
{
    // Class/static attribute for our default commission rate
    public static decimal DefaultCommissionRate { get; set; } = 0.05m;

    private decimal _commissionRate;

    public decimal CommissionRate
    {
        get => _commissionRate;
        set => _commissionRate = value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "Commission rate cannot be negative.")
            : value;
    }

    // Extent (collection of all Salesmen)
    private static readonly List<Salesman> _extent = new();
    public static IReadOnlyList<Salesman> Extent => _extent.AsReadOnly();

    public Salesman(
        string name,
        Address address,
        DateTime startDate,
        decimal salary,
        decimal? commissionRate = null,
        string? email = null)
        : base(name, address, startDate, salary, email)
    {
        CommissionRate = commissionRate ?? DefaultCommissionRate;
        _extent.Add(this);
    }
}
