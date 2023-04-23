namespace Hahn.Framework.Domain
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; set; }
        public AggregateRoot()
        {
            Id = Guid.NewGuid();
        }
    }
}
