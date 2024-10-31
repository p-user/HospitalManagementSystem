namespace Shared.DDD
{
    public interface IAggregate :IEntity
    {
        IReadOnlyList<IDomainEvent> Events { get; }
        IDomainEvent[] ClearDomainEvents();
    }

    public interface IAggregate<T>: IAggregate, IEntity<T>
    {

    }
}
