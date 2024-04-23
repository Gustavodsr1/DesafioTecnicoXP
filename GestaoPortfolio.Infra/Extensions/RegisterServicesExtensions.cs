using GestaoPortfolio.Application.Services;
using GestaoPortfolio.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoPortfolio.Infra.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IIncluirProdutoService, IncluirProdutoService>();
            services.AddTransient<IAlterarProdutoService, AlterarProdutoService>();
            services.AddTransient<IExcluirProdutoService, ExcluirProdutoService>();
            services.AddTransient<IIncluirPosicaoService, IncluirPosicaoService>();
            services.AddTransient<IAlterarPosicaoService, AlterarPosicaoService>();
            services.AddTransient<IExcluirPosicaoService, ExcluirPosicaoService>();
            services.AddTransient<IIncluirClienteService, IncluirClienteService>();
            services.AddTransient<IAlterarClienteService, AlterarClienteService>();
            services.AddTransient<IExcluirClienteService, ExcluirClienteService>();
            services.AddTransient<IIncluirOperacaoService, IncluirOperacaoService>();
            services.AddTransient<IAlterarOperacaoService, AlterarOperacaoService>();
            services.AddTransient<IExcluirOperacaoService, ExcluirOperacaoService>();
            services.AddTransient<IIncluirOfertaService, IncluirOfertaService>();
            services.AddTransient<IAlterarOfertaService, AlterarOfertaService>();
            services.AddTransient<IExcluirOfertaService, ExcluirOfertaService>();
            services.AddTransient<IAtualizarCarteiraEstoqueService, AtualizarCarteiraEstoqueService>();
        }
    }
}
