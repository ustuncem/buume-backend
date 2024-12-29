using BUUME.Application.Abstractions.Search;

namespace BUUME.Application.Districts.GetDistrictsByCityId;

public sealed record GetDistrictsByCityIdQuery(Guid CityId, string? SearchTerm) : ISearchableQuery<IReadOnlyList<DistrictResponse>>
{
}
