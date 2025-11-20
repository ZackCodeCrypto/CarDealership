namespace CarDealership.Domain;

public class FinancingPlan
{
    private static readonly List<FinancingPlan> _extent = new();
    public static IReadOnlyList<FinancingPlan> Extent => _extent.AsReadOnly();

    private decimal _monthlyPayment;
    private double _interestRate;

    public decimal MonthlyPayment
    {
        get => _monthlyPayment;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Monthly payment cannot be negative.");
            _monthlyPayment = value;
        }
    }

    public double InterestRate
    {
        get => _interestRate;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Interest rate cannot be negative.");
            _interestRate = value;
        }
    }

    public FinancingPlan(decimal monthlyPayment, double interestRate)
    {
        MonthlyPayment = monthlyPayment;
        InterestRate = interestRate;

        _extent.Add(this);
    }
}