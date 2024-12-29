using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Regions.GetRegionById;

public sealed record GetRegionByIdQuery(Guid RegionId) : IQuery<RegionResponse>
{
}
