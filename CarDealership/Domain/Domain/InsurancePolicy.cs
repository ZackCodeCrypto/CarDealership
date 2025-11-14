namespace CarDealership.Domain;

public class InsurancePolicy
{
    public string PolicyNumber { get; }
    public string Provider { get; }
    public string CoverageDetails { get; }
    public decimal Price { get; }

    public InsurancePolicy(string policyNumber, string provider, string coverageDetails, decimal price)
    {
        if (string.IsNullOrWhiteSpace(policyNumber))
            throw new ArgumentException("Policy number cannot be empty.", nameof(policyNumber));
        if (string.IsNullOrWhiteSpace(provider))
            throw new ArgumentException("Provider cannot be empty.", nameof(provider));
        if (string.IsNullOrWhiteSpace(coverageDetails))
            throw new ArgumentException("Coverage details cannot be empty.", nameof(coverageDetails));
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        PolicyNumber = policyNumber;
        Provider = provider;
        CoverageDetails = coverageDetails;
        Price = price;
    }
}
