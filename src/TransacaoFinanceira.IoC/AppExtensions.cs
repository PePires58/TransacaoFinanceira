using Microsoft.Extensions.DependencyInjection;
using TransacaoFinanceira.Aplicacao.Servicos;
using TransacaoFinanceira.DB.Contexto;
using TransacaoFinanceira.DB.Repositorios;
using TransacaoFinanceira.Dominio.Handlers;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Servicos;

namespace TransacaoFinanceira.IoC
{
    public static class AppExtensions
    {
        public static IServiceCollection ConfigurarDependencias(this IServiceCollection services)
        {
            return services
                .AddScoped<DbContextMock>((c) => new())
                .AddTransient<ITransacoesHandler, TransacoesHandler>()
                .AddTransient<IImprimirResultadoTransacoesService, ImprimirResultadoTransacoesService>()
                .AddTransient<IVerificaTransacaoService, VerificaTransacaoService>()
                .AddTransient<ITransacaoService, TransacaoService>()
                .AddTransient<IRepositorioContas, RepositorioContas>();
        }
    }
}
