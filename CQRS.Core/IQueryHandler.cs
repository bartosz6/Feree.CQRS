using CQRS.Core.Markers;

namespace CQRS.Core
{
    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler where TQuery : IQuery
    {
        TResult Handle(TQuery query);
    }
}