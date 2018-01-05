using System.Threading.Tasks;

namespace CQRS.Core.Query
{
    public interface IQueryDispatcher
    {
        Task<TResult> Dispatch<TResult>(IQuery<TResult> query);
    }
}