using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.TaxOffices.GetAllTaxOffices;

public sealed record GetAllTaxOfficesQuery() : IQuery<IReadOnlyList<TaxOfficeResponse>>
{
}
