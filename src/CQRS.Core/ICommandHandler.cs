using CQRS.Core.Markers;

namespace CQRS.Core
{
    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}