using CarDealership.Repositories;

namespace CarDealership.Domain;

public class ServiceRecord
{
    public DateTime ServiceDate { get; }
    public string Description { get; }
    public string ServiceType { get; }
    public double MilesDriven { get; }

    public static ExtentRepository<ServiceRecord> Extent = new();

    public ServiceRecord(DateTime serviceDate, string description, string serviceType, double milesDriven)
    {
        if (serviceDate > DateTime.Today)
            throw new ArgumentOutOfRangeException(nameof(serviceDate), "Service date cannot be in the future.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        if (string.IsNullOrWhiteSpace(serviceType))
            throw new ArgumentException("Service type cannot be empty.", nameof(serviceType));

        if (milesDriven < 0)
            throw new ArgumentOutOfRangeException(nameof(milesDriven), "Miles driven cannot be negative.");

        ServiceDate = serviceDate;
        Description = description;
        ServiceType = serviceType;
        MilesDriven = milesDriven;

        Extent.Add(this);
    }
}
