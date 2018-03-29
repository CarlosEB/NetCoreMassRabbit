using System;
using System.Threading.Tasks;
using MassTransit;
using NetCoreMassRabbit.Domain.Contracts;

namespace NetCoreMassRabbit.Worker1
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h => { });

                cfg.ReceiveEndpoint(host, "client-service", e =>
                {
                    e.Handler<ISubmitClient>(context =>
                    {
                        Console.WriteLine($"Receiving in work 1: {context.Message.Name}");
                        return context.RespondAsync<IClientAccepted>(new {context.Message.ClientId});
                    });
                });                
            });

            NewMethod(bus).Wait();

        }

        private static async Task NewMethod(IBusControl bus)
        {
            await bus.StartAsync();
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
                await bus.StopAsync();
            }
        }
    }
}
