using Microsoft.Extensions.DependencyInjection;
using System;
using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Handlers;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.IoC;

namespace TransacaoFinanceira
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection sc = new ServiceCollection();
            sc.ConfigurarDependencias();
            var provider = sc.BuildServiceProvider();

            ITransacoesHandler transacaoService = provider.GetService<ITransacoesHandler>();
            IImprimirResultadoTransacoesService imprimirResultadoTransacoesService = provider.GetService<IImprimirResultadoTransacoesService>();

            Transacao[] transacoes =
            [
                new Transacao(1, Convert.ToDateTime("09/09/2023 14:15:00"), 938485762, 2147483649,150),
                new Transacao(2, Convert.ToDateTime("09/09/2023 14:15:05"), 2147483649, 210385733, 149),
                new Transacao(3, Convert.ToDateTime("09/09/2023 14:15:29"), 347586970, 238596054, 1100),
                new Transacao(4, Convert.ToDateTime("09/09/2023 14:17:00"), 675869708, 210385733, 5300),
                new Transacao(5, Convert.ToDateTime("09/09/2023 14:18:00"), 238596054, 674038564, 1489),
                new Transacao(6, Convert.ToDateTime("09/09/2023 14:18:20"), 573659065, 563856300, 49),
                new Transacao(7, Convert.ToDateTime("09/09/2023 14:19:00"), 938485762, 2147483649, 44),
                new Transacao(8, Convert.ToDateTime("09/09/2023 14:19:01"), 573659065, 675869708, 150),
            ];

            transacaoService.RealizarTransacoes(transacoes);
            imprimirResultadoTransacoesService.Imprimir(transacoes);

        }
    }
}
