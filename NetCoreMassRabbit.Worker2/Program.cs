using NetCoreMassRabbit.Infrastructure;

namespace NetCoreMassRabbit.Worker2
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new Worker(3);
            worker.RunAsync().Wait();
        }
    }
}
