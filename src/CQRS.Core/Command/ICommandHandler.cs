using System.Threading.Tasks;

namespace CQRS.Core.Command
{
    public interface IAsyncCommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
    
    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
    
    public interface ICommandHandler {}
}