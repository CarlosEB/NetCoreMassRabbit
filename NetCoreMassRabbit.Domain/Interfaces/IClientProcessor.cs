using NetCoreMassRabbit.Domain.Contracts;
using System.Threading.Tasks;

namespace NetCoreMassRabbit.Domain.Interfaces
{
    public interface IClientProcessor
    {
        Task<ISubmitClient> Process(int workNumber, ISubmitClient message);
    }
}