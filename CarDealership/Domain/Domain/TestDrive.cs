namespace CarDealership.Domain;

public class TestDrive
{
    private static readonly List<TestDrive> _extent = new();
    public static IReadOnlyList<TestDrive> Extent => _extent.AsReadOnly();

    private int _duration;
    private int _distance;
    private const int MaxDistance = 20;

    public DateTime Date { get; }
    public int Duration
    {
        get => _duration;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Duration must be positive.");
            _duration = value;
        }
    }

    public int Distance
    {
        get => _distance;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Distance cannot be negative.");
            if (value > MaxDistance)
                throw new ArgumentException("Distance exceeds maximum allowed for test drive.");
            _distance = value;
        }
    }

    public Customer Customer { get; }
    public Car Car { get; }

    public TestDrive(DateTime date, int duration, int distance, Customer customer, Car car)
    {
        if (date > DateTime.Today)
            throw new ArgumentException("Test drive cannot be scheduled in the future.");

        Date = date;
        Duration = duration;
        Distance = distance;
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Car = car ?? throw new ArgumentNullException(nameof(car));

        _extent.Add(this);
    }
}