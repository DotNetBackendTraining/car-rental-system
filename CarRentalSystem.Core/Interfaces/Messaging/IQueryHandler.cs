using CarRentalSystem.Core.Shared;
using MediatR;

namespace CarRentalSystem.Core.Interfaces.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;