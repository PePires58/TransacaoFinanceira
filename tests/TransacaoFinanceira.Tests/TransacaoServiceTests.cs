using FluentAssertions;
using TransacaoFinanceira.Aplicacao.Servicos;
using TransacaoFinanceira.DB.Repositorios;
using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Enums;

namespace TransacaoFinanceira.Tests
{
    [TestClass]
    public class TransacaoServiceTests
    {
        ITransacaoService TransacaoService { get; }
        IRepositorioContas RepositorioContas { get; }
        public TransacaoServiceTests()
        {

            RepositorioContas = new RepositorioContas(new());
            TransacaoService = new TransacaoService(RepositorioContas);
        }

        [TestMethod]
        public void NaoDeveRealizarTransacaoPorFaltaDeSaldo()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100);
            transacao.SetContasTransacao(new(1, 0), new(2, 200));

            TransacaoService.EfetivarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.SaldoInsuficiente);
        }

        [TestMethod]
        public void DeveRealizarTransacao() {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100);
            transacao.SetContasTransacao(new(1, 200), new(2, 200));

            TransacaoService.EfetivarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.Processada);
        }
    }
}
