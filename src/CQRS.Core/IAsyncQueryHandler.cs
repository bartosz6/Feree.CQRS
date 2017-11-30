using System.Threading.Tasks;

namespace CQRS.Core
{
    public interface IAsyncQueryHandler<in TQuery, TResult> : IQueryHandler<TQuery, Task<TResult>> where TQuery : IQuery
    {
    }
}