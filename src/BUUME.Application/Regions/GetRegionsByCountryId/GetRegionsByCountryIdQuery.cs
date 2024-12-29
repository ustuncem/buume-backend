using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.Regions.GetRegionsByCountryId;

public sealed record GetRegionsByCountryIdQuery(Guid CountryId, string? SearchTerm) : ISearchableQuery<IReadOnlyList<RegionResponse>>
{
}
