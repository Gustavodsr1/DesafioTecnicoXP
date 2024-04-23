using Confluent.Kafka;
using GestaoPortfolio.Domain.Kafka;
using Microsoft.Extensions.Configuration;

namespace GestaoPortfolio.Application.Kafka
{
    public abstract class ConsumidorKafka : IConsumidorKafka
    {
        private readonly IConfiguration configuration;

        protected ConsumidorKafka(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected abstract Task Processar(string mensagem);

        public async Task Consumir(string topico, CancellationToken cancellationToken = default)
        {
            ConsumerConfig consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = configuration.GetValue<string>("Kafka:BrokerUrl"),
                GroupId = Guid.NewGuid().ToString()
            };

            ConsumerBuilder<string, string> consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig);
            using (IConsumer<string, string> consumer = consumerBuilder.Build())
            {
                consumer.Subscribe(topico);

                while (!cancellationToken.IsCancellationRequested)
                {
                    ConsumeResult<string, string> consumeResult = consumer.Consume(cancellationToken);
                    try
                    {
                        await Processar(consumeResult.Message.Value);
                    }
                    catch (Exception)
                    {
                        //ignora
                    }
                }
            }
        }
    }
}
