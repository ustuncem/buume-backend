namespace BUUME.Api.Requests;

public abstract class SearchableRequest
{
    public string? searchTerm { get; set; } = string.Empty;
}