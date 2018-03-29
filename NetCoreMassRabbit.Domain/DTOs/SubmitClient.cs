using NetCoreMassRabbit.Domain.Contracts;

namespace NetCoreMassRabbit.Domain.DTOs
{
    public class SubmitClient : ISubmitClient
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
    }
}
