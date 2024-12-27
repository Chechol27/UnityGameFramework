namespace Services.Core
{
    public interface IIndexedManagedService : IManagedService
    {
        int Order { get; set; }
    }
}