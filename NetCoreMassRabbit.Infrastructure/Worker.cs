using MassTransit;
using NetCoreMassRabbit.Domain.Contracts;
using NetCoreMassRabbit.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace NetCoreMassRabbit.Infrastructure
{
    public class Worker : IWorker
    {
        private readonly IBusControl _bus;
        private readonly IClientProcessor _clientProcessor;

        public Worker(int workNumber)
        {
            _clientProcessor = new ClientProcessor();
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://192.168.99.100/"), h => { });

                cfg.ReceiveEndpoint(host, "client-service", e =>
                {
                    e.Handler<ISubmitClient>(context =>
                    {
                        return _clientProcessor.Process(workNumber, context.Message);
                    });
                });
            });
        }

        public async Task RunAsync()
        {
            await _bus.StartAsync();
            try
            {
                Console.WriteLine("Working....");

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                await _bus.StopAsync();
            }
        }
    }
}
