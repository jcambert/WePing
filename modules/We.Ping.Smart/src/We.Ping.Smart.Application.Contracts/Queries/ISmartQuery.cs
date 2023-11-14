using Mediator;
using We.Results;

namespace We.Ping.Queries;

public interface ISmartQuery
{

}


public interface ISmartQuery<TResponse> : ISmartQuery, WeM.IQuery<TResponse>, IRequest<Result<TResponse>>, IBaseRequest, IMessage where TResponse : SmartResponse
{
}

public abstract class SmartQuery : ISmartQuery
{
}