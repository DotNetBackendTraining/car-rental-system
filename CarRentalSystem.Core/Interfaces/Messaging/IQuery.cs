using CarRentalSystem.Core.Shared;
using MediatR;

namespace CarRentalSystem.Core.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;