using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TQuery, TResult>(TQuery query) where TQuery : IQuery;
        
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}