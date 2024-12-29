using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Districts.GetDistrictById;

public sealed record GetDistrictByIdQuery(Guid DistrictId) : IQuery<DistrictResponse>
{
}
