namespace BUUME.Application.Abstractions.Files;

public sealed record FileResponse(string Name, double Size, string Path, string Type);