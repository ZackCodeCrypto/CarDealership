using CarDealership.Repositories;

namespace CarDealership.Domain;

public class Warranty
{
    private string _terms;

    public string Terms
    {
        get => _terms;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Warranty terms cannot be empty.");
            _terms = value;
        }
    }

    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    // Derived attribute
    public bool IsActive => DateTime.Today >= StartDate && DateTime.Today <= EndDate;

    public static ExtentRepository<Warranty> Extent = new();

    public Warranty(DateTime startDate, DateTime endDate, string terms)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date cannot be after end date.");

        if (endDate < DateTime.Today.AddYears(-30))
            throw new ArgumentOutOfRangeException(nameof(endDate), "End date unrealistically old.");

        StartDate = startDate;
        EndDate = endDate;
        Terms = terms;

        Extent.Add(this);
    }
}