using CarRentalSystem.Core.Shared;
using MediatR;

namespace CarRentalSystem.Core.Interfaces.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;