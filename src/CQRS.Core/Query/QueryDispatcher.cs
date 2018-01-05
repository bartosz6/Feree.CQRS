using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly Func<IQuery, IQueryHandler> _handlerProducer;

        public QueryDispatcher(Func<IQuery, IQueryHandler> handlerProducer)
        {
            _handlerProducer = handlerProducer;
        }

        [DebuggerStepThrough]
        public async Task<TResult> Dispatch<TResult>(IQuery<TResult> query)
        {
            switch (_handlerProducer(query))
            {
                case BaseQueryHandler<Task<TResult>> asynchronous:
                    return await asynchronous.BaseHandle(query);
                case BaseQueryHandler<TResult> synchronous:
                    return synchronous.BaseHandle(query);
                default:
                    throw new InvalidOperationException($"handler for query {query.GetType().FullName} not found");
            }
        }
    }
}