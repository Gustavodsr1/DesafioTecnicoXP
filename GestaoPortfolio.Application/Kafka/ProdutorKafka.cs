using Confluent.Kafka;
using GestaoPortfolio.Domain.Kafka;
using Microsoft.Extensions.Configuration;

namespace GestaoPortfolio.Application.Kafka
{
    public class ProdutorKafka : IProdutorKafka
    {
        private readonly IConfiguration configuration;

        public ProdutorKafka(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task ProduzirMensagem(string topico, string chave, string mensagemJson)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration.GetValue<string>("Kafka:BrokerUrl"),
            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                await producer.ProduceAsync(topico, new Message<string, string> { Key = chave, Value = mensagemJson });
                producer.Flush(TimeSpan.FromSeconds(5));
            }
        }
    }
}
