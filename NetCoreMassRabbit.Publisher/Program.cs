using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NetCoreMassRabbit.Domain.Contracts;
using NetCoreMassRabbit.Domain.DTOs;

namespace NetCoreMassRabbit.Publisher
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static readonly Uri ServiceAddress = new Uri("rabbitmq://192.168.99.100/client-service");
        static void Main(string[] args)
        {
            StartBus();

            Console.WriteLine("Press enter to start.");

            Console.ReadKey();

            //PublishClient();

            SendClient();
        }

        private static void StartBus()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://192.168.99.100/"), h => { });
            });

            var timeout = TimeSpan.FromSeconds(10);

            var services = new ServiceCollection()
            .AddSingleton<IPublishEndpoint>(bus)
            .AddSingleton<ISendEndpointProvider>(bus)
            .AddSingleton<IBus>(bus)
            .AddScoped<IRequestClient<ISubmitClient, IClientAccepted>>(x => new MessageRequestClient<ISubmitClient, IClientAccepted>(x.GetRequiredService<IBus>(), ServiceAddress, timeout));

            _serviceProvider = services.BuildServiceProvider();

            bus.Start();
        }

        private static void SendClient()
        {
            var sendEndpointProvider = _serviceProvider.GetService<ISendEndpointProvider>();

            var endpoint = sendEndpointProvider.GetSendEndpoint(ServiceAddress).Result;
            for (var i = 1; i <= 5000; i++)
            {
                var name = $"Name {i}";
                Console.WriteLine($"Sending {name}");
                endpoint.Send(new SubmitClient { ClientId = i, Name = name });
            }
        }

        private static void PublishClient()
        {
            var publishClient = _serviceProvider.GetService<IRequestClient<ISubmitClient, IClientAccepted>>();

            for (var i = 1; i <= 50; i++)
            {
                var name = $"Name {i}";
                Console.WriteLine($"Sending {name}");
                publishClient.Request(new { ClientId = i, Name = name });
            }
        }
    }
}
