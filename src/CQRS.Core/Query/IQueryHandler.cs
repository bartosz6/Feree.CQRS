using System.Threading.Tasks;
using CQRS.Core.Markers;

namespace CQRS.Core.Query
{
    public interface IAsyncQueryHandler<in TQuery, TResult> : IQueryHandler<TQuery, Task<TResult>> where TQuery : IQuery
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}