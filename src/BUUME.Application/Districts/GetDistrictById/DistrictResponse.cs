namespace BUUME.Application.Districts.GetDistrictById;

public sealed record MinimizedCity
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
}

public sealed record DistrictResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
    public string Code { get; init; } = default!;
    
    public MinimizedCity City { get; set; } = default!;
}