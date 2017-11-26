using System.Threading.Tasks;
using CQRS.Core.Markers;

namespace CQRS.Core
{
    public interface IAsyncCommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}