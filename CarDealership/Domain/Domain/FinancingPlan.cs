using CarDealership.Repositories;

namespace CarDealership.Domain;

public class FinancingPlan
{
    private decimal _monthlyPayment;
    private double _interestRate;
    private HashSet<Sale> _sales = [];

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

    public IReadOnlyCollection<Sale> Sales => _sales.ToList().AsReadOnly();

    public static ExtentRepository<FinancingPlan> Extent = new();

    public FinancingPlan(decimal monthlyPayment, double interestRate)
    {
        MonthlyPayment = monthlyPayment;
        InterestRate = interestRate;

        Extent.Add(this);
    }

    public void AssignToSale(Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale);

        if (_sales.Contains(sale))
            return;

        _sales.Add(sale);
        sale.AddFinancingPlan(this);
    }

    public void RemoveSale(Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale);

        if (_sales.Remove(sale))
        {
            sale.RemoveFinancingPlan();
        }
    }
}