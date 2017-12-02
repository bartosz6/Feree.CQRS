using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CQRS.Core.Markers;

namespace CQRS.Core
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly ReadOnlyDictionary<Type, IQueryHandler> _queryHandlersDictionary;

        public QueryDispatcher(params IQueryHandler[] queryHandlers)
        {
            var interfaceId = typeof(IQueryHandler<,>).GUID;
            _queryHandlersDictionary = new ReadOnlyDictionary<Type, IQueryHandler>(
                queryHandlers
                    .Select(handler =>
                        handler.GetType().GetTypeInfo().ImplementedInterfaces
                            .Where(@interface => @interface.GUID == interfaceId)
                            .Select(@interface => @interface.GetTypeInfo().GenericTypeArguments.First())
                            .Select(queryType => (queryType, handler)))
                    .SelectMany(_ => _)
                    .GroupBy(x => x.queryType)
                    .Select(x => x.Count() > 1
                        ? throw new ArgumentException($"Query handler for {x.Key.Name} has been already registered.")
                        : x.AsEnumerable())
                    .SelectMany(_ => _)
                    .ToDictionary(key => key.queryType, value => value.handler)
            );
        }

        [DebuggerStepThrough]
        public TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            return _queryHandlersDictionary.ContainsKey(typeof(TQuery)) &&
                   _queryHandlersDictionary[typeof(TQuery)] is IQueryHandler<TQuery, TResult> handler
                ? handler.Handle(query)
                : throw new InvalidOperationException(
                    $"query handler for query<{typeof(TQuery).Name}, {typeof(TResult).Name}> not found");
        }

        [DebuggerStepThrough]
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery =>
            Dispatch<TQuery, Task<TResult>>(query);
    }
}