using MediatR;

namespace Shared.DDD
{
    public interface IDomainEvent : INotification
    {

        Guid EventId => Guid.NewGuid();
        public DateTime Occurredon => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}
