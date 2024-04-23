using Confluent.Kafka;
using Confluent.Kafka.Admin;
using GestaoPortfolio.Application.Kafka;
using GestaoPortfolio.Domain.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Infra.Extensions
{
    public static class RegisterKafkaExtensions
    {
        public static void RegisterKafka(this IServiceCollection services, IConfiguration configuration)
        {
            IAdminClient client = new AdminClientBuilder(new AdminClientConfig()
            {
                BootstrapServers = configuration.GetValue<string>("Kafka:BrokerUrl")
            }).Build();

            List<TopicSpecification> topicSpecs = new List<TopicSpecification>()
            {
                new()
                {
                    Name = "ORDEM_COMPRA",
                    NumPartitions = 1,
                    ReplicationFactor = 1
                },
                new()
                {
                    Name = "ORDEM_VENDA",
                    NumPartitions = 1,
                    ReplicationFactor = 1
                }
            };

            foreach (var topicSpec in topicSpecs)
            {
                try
                {
                    client.CreateTopicsAsync(new List<TopicSpecification>() { topicSpec }).GetAwaiter().GetResult();
                }
                catch (Exception)
                {
                }
            }

            services.AddTransient<IProdutorKafka, ProdutorKafka>();
            services.AddTransient<IOrdemCompraConsumidor, OrdemCompraConsumidor>();
            services.AddTransient<IOrdemVendaConsumidor, OrdemVendaConsumidor>();
        }
    }
}
