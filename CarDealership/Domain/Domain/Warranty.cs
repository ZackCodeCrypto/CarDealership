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
    
    // Reference to Car for composition
    public Car? Car { get; private set; }
    
    public static ExtentRepository<Warranty> Extent = new();

    public Warranty(DateTime startDate, DateTime endDate, string terms)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date cannot be after end date.");
        
        StartDate = startDate;
        EndDate = endDate;
        Terms = terms;

        Extent.Add(this);
    }
    
    internal void LinkToCar(Car car)
    {
        Car = car;
    }

    internal void Delete()
    {
        Car = null;
        Extent.Remove(this);
    }
}


