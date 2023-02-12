using ErrorOr;
using MediatR;

namespace LDST.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}