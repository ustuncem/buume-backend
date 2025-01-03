﻿namespace BUUME.Application.Countries.GetCountryById;

public sealed record CountryResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string Code { get; init; } = default!;

    public bool HasRegion { get; init; }
}