using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Markers;

namespace CQRS.Core
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IEnumerable<IQueryHandler> _queryHandlers;

        public QueryDispatcher(params IQueryHandler[] queryHandlers)
        {
            _queryHandlers = queryHandlers;
        }
        
        [DebuggerStepThrough]
        public TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var queryHandler = (IQueryHandler<TQuery, TResult>) _queryHandlers.Single(handler => handler is IQueryHandler<TQuery, TResult>);
            return queryHandler.Handle(query);
        }

        [DebuggerStepThrough]
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            var queryHandler = (IAsyncQueryHandler<TQuery, TResult>) _queryHandlers.Single(handler => handler is IAsyncQueryHandler<TQuery, TResult>);
            return queryHandler.HandleAsync(query);
        }
    }
}