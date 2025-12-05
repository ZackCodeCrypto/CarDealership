using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Salesman : Employee
{
    private decimal _commissionRate;

    public decimal CommissionRate
    {
        get => _commissionRate;
        set => _commissionRate = value < 0
            ? throw new ArgumentOutOfRangeException(nameof(value), "Commission rate cannot be negative.")
            : value;
    }

    // Class/static attribute for our default commission rate
    public static decimal DefaultCommissionRate { get; set; } = 0.05m;

    public static ExtentRepository<Salesman> Extent = new();

    public Salesman(
        string name,
        string phoneNumber,
        decimal? commissionRate = null,
        string? email = null)
        : base(name, phoneNumber, email)
    {
        CommissionRate = commissionRate ?? DefaultCommissionRate;

        Extent.Add(this);
    }
}
