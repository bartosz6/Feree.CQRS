using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Markers;

namespace CQRS.Core
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly HashSet<IQueryHandler> _queryHandlers;

        public QueryDispatcher(params IQueryHandler[] queryHandlers)
        {
            _queryHandlers = new HashSet<IQueryHandler>(queryHandlers);
        }
        
        [DebuggerStepThrough]
        public TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            foreach (var queryHandler in _queryHandlers)
            {
                if (queryHandler is IQueryHandler<TQuery, TResult> han)
                    return han.Handle(query);
            }
            throw new InvalidOperationException($"query handler for query<{typeof(TQuery).Name}, {typeof(TResult).Name}> not found");
        }

        [DebuggerStepThrough]
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery =>
            Dispatch<TQuery, Task<TResult>>(query);
    }
}