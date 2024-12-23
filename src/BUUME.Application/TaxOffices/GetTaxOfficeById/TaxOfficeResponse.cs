namespace BUUME.Application.TaxOffices.GetTaxOfficeById;

public sealed record TaxOfficeResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}
