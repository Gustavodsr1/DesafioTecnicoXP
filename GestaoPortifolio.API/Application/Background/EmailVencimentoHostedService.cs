using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Repository;
using GestaoPortfolio.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace GestaoPortfolio.Application.Background
{
    public class EmailVencimentoHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public EmailVencimentoHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                TimeSpan waitTime = default;

                using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
                {
                    IInvestimentosRepository carteiraRepository = serviceScope.ServiceProvider.GetRequiredService<IInvestimentosRepository>();

                    IJobRepository jobRepository = serviceScope.ServiceProvider.GetRequiredService<IJobRepository>();
                    Job job = await jobRepository.GetSiglaAsync("EMAIL_VCTO");

                    if (job.ProximaExecucao == null || DateTime.Now > job.ProximaExecucao.Value)
                    {
                        try
                        {
                            IEnumerable<Investimentos> posicoesAVencer = await carteiraRepository.ListarVencimentos(DateTime.Today);

                            if (posicoesAVencer.Any())
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine($"Posições com vencimento na data atual [ {DateTime.Today:d} ]");
                                foreach (Investimentos posicao in posicoesAVencer)
                                {
                                    sb.AppendLine($"PAPEL: {posicao.Papel} VALOR: {posicao.ValorTotalOperacao}");
                                }

                                SmtpClient smtpClient = new()
                                {
                                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                                    PickupDirectoryLocation = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName
                                };

                                MailMessage mailMessage = new()
                                {
                                    Subject = "Posições a vencer",
                                    Body = sb.ToString(),
                                    Sender = new MailAddress("ribeirogustavodev@gmail.com.br"),
                                };

                                mailMessage.To.Add(new MailAddress("mesa@xpi.com.br"));

                                using (smtpClient)
                                {
                                    await smtpClient.SendMailAsync(mailMessage, stoppingToken);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                        }
                    }

                    job.UltimaExecucao = DateTime.Now;
                    job.CalcularProximaExecucao();

                    waitTime = job.ProximaExecucao.Value - job.UltimaExecucao.Value;

                    await jobRepository.Update(job);
                }

                await Task.Delay(waitTime, stoppingToken);
            }
        }
    }
}
