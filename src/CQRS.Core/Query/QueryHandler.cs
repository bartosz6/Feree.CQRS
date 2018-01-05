using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public abstract class AsyncQueryHandler<TQuery, TResult> : BaseQueryHandler<Task<TResult>>
        where TQuery : IQuery<TResult>
    {
        protected abstract Task<TResult> Handle(TQuery query);

        internal override Task<TResult> BaseHandle(object query) => Handle((TQuery) query);
    }

    public abstract class QueryHandler<TQuery, TResult> : BaseQueryHandler<TResult> where TQuery : IQuery<TResult>
    {
        protected abstract TResult Handle(TQuery query);

        internal override TResult BaseHandle(object query) => Handle((TQuery) query);
    }

    public abstract class BaseQueryHandler<TResult> : IQueryHandler
    {
        internal abstract TResult BaseHandle(object query);
    }
}