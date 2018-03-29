namespace NetCoreMassRabbit.Domain.Contracts
{
    public interface ISubmitClient
    {
        int ClientId { get; set; }
        string Name { get; set; }
    }
}
