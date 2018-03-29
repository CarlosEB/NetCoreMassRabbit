using NetCoreMassRabbit.Domain.Contracts;
using NetCoreMassRabbit.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace NetCoreMassRabbit.Infrastructure
{
    public class ClientProcessor : IClientProcessor
    {
        public Task<ISubmitClient> Process(int workNumber, ISubmitClient message)
        {
            var result = Task.Run(() =>
            {
                Console.WriteLine($"Receiving in work {workNumber}: {message.Name}");
                return message;
            });

            return result;            
        }
    }
}
