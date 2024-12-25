using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Abstractions.Search;

public interface ISearchableQuery<TResponse> : IQuery<TResponse>
{
    string? SearchTerm { get; }
}