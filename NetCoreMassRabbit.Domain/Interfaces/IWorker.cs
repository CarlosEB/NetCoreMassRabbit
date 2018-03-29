using System.Threading.Tasks;

namespace NetCoreMassRabbit.Domain.Interfaces
{
    public interface IWorker
    {
        Task RunAsync();
    }
}