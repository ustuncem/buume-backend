using BUUME.SharedKernel;
using MediatR;

namespace BUUME.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;