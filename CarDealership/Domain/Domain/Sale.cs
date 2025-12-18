using CarDealership.Repositories;

namespace CarDealership.Domain;


public class Sale
{
    private DateTime _saleDate;
    public string MethodOfPayment { get; set; }
    public Customer Customer { get; }
    public Employee Salesman { get; }
    public Car? Car { get; }
    public IList<Accessory> Accessories { get; } = new List<Accessory>();
    public InsurancePolicy? InsurancePolicy { get; }
    public FinancingPlan? FinancingPlan { get; private set; }

    // derived attribute: /totalPrice
    public decimal TotalPrice
    {
        get
        {
            decimal total = 0;
            if (Car != null) total += Car.Price;
            if (InsurancePolicy != null) total += InsurancePolicy.Price;
            total += Accessories.Sum(a => a.Price);
            return total;
        }
    }
    public DateTime SaleDate
    {
        get => _saleDate;
        set
        {
            if (value > DateTime.Today)
                throw new ArgumentOutOfRangeException(nameof(value), "Sale date cannot be in the future.");
            _saleDate = value;
        }
    }

    public static ExtentRepository<Sale> Extent = new();

    public Sale(
        DateTime saleDate,
        string methodOfPayment,
        Customer customer,
        Employee salesman,
        Car? car = null,
        InsurancePolicy? insurancePolicy = null,
        IEnumerable<Accessory>? accessories = null)
    {
        SaleDate = saleDate;
        MethodOfPayment = methodOfPayment;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Salesman = salesman ?? throw new ArgumentNullException(nameof(salesman));
        Car = car;
        InsurancePolicy = insurancePolicy;

        if (accessories != null)
            foreach (var a in accessories)
                Accessories.Add(a);
    }

    public void AddFinancingPlan(FinancingPlan plan)
    {
        ArgumentNullException.ThrowIfNull(plan);

        if (FinancingPlan == plan)
            return;

        if (FinancingPlan != null)
            throw new InvalidOperationException("A financing plan is already assigned to this sale.");

        FinancingPlan = plan;
        plan.AssignToSale(this);
    }

    public void RemoveFinancingPlan()
    {
        if (FinancingPlan == null)
            return;

        var plan = FinancingPlan;
        FinancingPlan = null;
        plan.RemoveSale(this);
    }
}
