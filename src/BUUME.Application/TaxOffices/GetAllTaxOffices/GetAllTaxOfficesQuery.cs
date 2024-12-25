using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.TaxOffices.GetAllTaxOffices;

public sealed record GetAllTaxOfficesQuery(string? SearchTerm) : ISearchableQuery<IReadOnlyList<TaxOfficeResponse>>
{
}
