using NetCoreMassRabbit.Infrastructure;
using System.Threading.Tasks;

namespace NetCoreMassRabbit.Worker1
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskW1 = Task.Run(() =>
            {
                var worker = new Worker(1);
                worker.RunAsync().Wait();
            });

            var taskW2 = Task.Run(() =>
            {
                var worker = new Worker(2);
                worker.RunAsync().Wait();
            });

            var taskW3 = Task.Run(() =>
            {
                var worker = new Worker(3);
                worker.RunAsync().Wait();
            });

            var taskW4 = Task.Run(() =>
            {
                var worker = new Worker(4);
                worker.RunAsync().Wait();
            });

            Task.WaitAll(taskW1, taskW2, taskW3, taskW4);
        }
    }
}
