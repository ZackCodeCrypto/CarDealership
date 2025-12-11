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
    public Car? Car { get; set; }
    
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
    
    public void LinkToCar(Car car)
    {
        if (car == null)
            throw new ArgumentNullException(nameof(car));

        if (Car != null && Car != car)
            throw new InvalidOperationException("This warranty is already linked to another car.");

        Car = car;

        // reverse connection
        if (car.Warranty != this)
            car.AssignWarranty(this);
    }
    
    public void RemoveFromCar()
    {
        if (Car == null)
            return;

        var previous = Car;
        Car = null;

        if (previous.Warranty != null)
            previous.RemoveWarranty();

        Extent.Remove(this);
    }
}


