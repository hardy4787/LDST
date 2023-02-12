using ErrorOr;
using MediatR;

namespace LDST.Application.Abstractions;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
