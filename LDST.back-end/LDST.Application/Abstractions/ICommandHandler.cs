using ErrorOr;
using MediatR;

namespace LDST.Application.Abstractions;

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
}

