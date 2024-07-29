using FluentAssertions;
using TransacaoFinanceira.Aplicacao.Servicos;
using TransacaoFinanceira.DB.Contexto;
using TransacaoFinanceira.DB.Repositorios;
using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Handlers;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Entidades;
using TransacaoFinanceira.Servicos;

namespace TransacaoFinanceira.Tests
{
    [TestClass]
    public class TransacoesHandlerTests
    {
        ITransacoesHandler TransacoesHandler { get; }
        IRepositorioContas RepositoryContas { get; }
        IVerificaTransacaoService VerificarExistenciaContasService { get; }
        ITransacaoService TransacaoService { get; }

        DbContextMock DbContext { get; }

        public TransacoesHandlerTests()
        {
            DbContext = new();
            RepositoryContas = new RepositorioContas(DbContext);
            VerificarExistenciaContasService = new VerificaTransacaoService(RepositoryContas);
            TransacaoService = new TransacaoService(RepositoryContas);

            TransacoesHandler = new TransacoesHandler(RepositoryContas, VerificarExistenciaContasService, TransacaoService);
        }

        [TestMethod]
        public void DeveExecutarTransacoes()
        {
            Transacao[] transacoes =
            [
               new Transacao(1, Convert.ToDateTime("09/09/2023 14:15:00"), 938485762, 2147483649, 150),
                new Transacao(2, Convert.ToDateTime("09/09/2023 14:15:05"), 2147483649, 210385733, 149),
                new Transacao(3, Convert.ToDateTime("09/09/2023 14:15:29"), 347586970, 238596054, 1100),
                new Transacao(4, Convert.ToDateTime("09/09/2023 14:17:00"), 675869708, 210385733, 5300),
                new Transacao(5, Convert.ToDateTime("09/09/2023 14:18:00"), 238596054, 674038564, 1489),
                new Transacao(6, Convert.ToDateTime("09/09/2023 14:18:20"), 573659065, 563856300, 49),
                new Transacao(7, Convert.ToDateTime("09/09/2023 14:19:00"), 938485762, 2147483649, 44),
                new Transacao(8, Convert.ToDateTime("09/09/2023 14:19:01"), 573659065, 675869708, 150),
            ];

            TransacoesHandler.RealizarTransacoes(transacoes);

            HashSet<long> idsContas = [];
            foreach (var item in transacoes)
            {
                idsContas.Add(item.IdContaDestino);
                idsContas.Add(item.IdContaOrigem);
            }

            Dictionary<long, Conta> contasDb = [];
            foreach (var item in idsContas)
                contasDb.Add(item, RepositoryContas.Consultar(item));
            Dictionary<long, Conta> contasExpected = new()
            {
                { 938485762, new(938485762, 30) },
                { 2147483649, new(2147483649, 1) },
                { 210385733, new(210385733, 159) },
                { 347586970, new(347586970, 100) },
                { 675869708, new(675869708,5050) },
                { 238596054, new(238596054,89) },
                { 573659065, new(573659065,588) },

                { 674038564, new(674038564,1889) },
                { 563856300, new(563856300,1249) }
            };


            foreach (var item in contasExpected)
            {
                var contaDb = contasDb[item.Key];

                contaDb.Should().Be(item.Value);
            }
        }
    }
}