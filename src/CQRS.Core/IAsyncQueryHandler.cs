using System.Threading.Tasks;
using CQRS.Core.Markers;

namespace CQRS.Core
{
    public interface IAsyncQueryHandler<in TQuery, TResult> : IQueryHandler where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}