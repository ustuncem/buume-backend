namespace BUUME.Application.Districts.GetDistrictsByCityId;

public sealed record DistrictResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
}