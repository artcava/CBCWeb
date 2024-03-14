using MediatR;

namespace CenturyBelongingCalculator.Application.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
{
}
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
{
}
public interface IIdempotentCommand<out TResponse> : ICommand<TResponse>
{
    int RequestId { get; set; }
}
