namespace BUUME.Application.TaxOffices.GetAllTaxOffices;

public sealed record TaxOfficeResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}
