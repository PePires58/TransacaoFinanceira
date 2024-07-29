using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Aplicacao.Servicos;
using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Entidades;
using TransacaoFinanceira.Enums;

namespace TransacaoFinanceira.Tests
{
    [TestClass]
    public class ImprimirResultadoTransacoesServiceTests
    {
        IImprimirResultadoTransacoesService ImprimirResultadoTransacoesService { get; }

        public ImprimirResultadoTransacoesServiceTests()
        {
            ImprimirResultadoTransacoesService = new ImprimirResultadoTransacoesService();
        }

        [TestMethod]
        public void TestAguardandoProcessamento()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetStatus(EStatusTransacao.AguardandoProcessamento);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} aguardando processamento");
        }

        [TestMethod]
        public void TestEmProcessamento()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetStatus(EStatusTransacao.EmProcessamento);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} em processamento");
        }

        [TestMethod]
        public void TestProcessada()
        {
            Conta contaOrigem = new(1, 500.00m);
            Conta contaDestino = new(2, 300.00m);
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetContasTransacao(contaOrigem, contaDestino);
            transacao.SetStatus(EStatusTransacao.Processada);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} foi efetivada com sucesso! Novos saldos: Conta Origem:{transacao.ContaOrigem.Saldo} | Conta Destino: {transacao.ContaDestino.Saldo}");
        }

        [TestMethod]
        public void TestSaldoInsuficiente()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetStatus(EStatusTransacao.SaldoInsuficiente);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} foi cancelada por falta de saldo");
        }

        [TestMethod]
        public void TestSaldoDesatualizadoRetry()
        {
            Transacao transacao = new Transacao(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetStatus(EStatusTransacao.SaldoDesatualizadoRetry);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} nao foi efetivada pois o saldo está desatualizando, iremos tentar novamente");
        }

        [TestMethod]
        public void TestContaOrigemNaoExiste()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetStatus(EStatusTransacao.ContaOrigemNaoExiste);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} nao foi efetivada pois conta de origem {transacao.IdContaOrigem} nao existe");
        }

        [TestMethod]
        public void TestContaDestinoNaoExiste()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, 100.00m);
            transacao.SetStatus(EStatusTransacao.ContaDestinoNaoExiste);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} nao foi efetivada pois conta de destino {transacao.IdContaDestino} nao existe");
        }

        [TestMethod]
        public void TestContasIguais()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 1, 100.00m);
            transacao.SetStatus(EStatusTransacao.ContasIguais);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} nao foi efetivada pois conta de origem e destino sao iguais");
        }

        [TestMethod]
        public void TestValorTransacaoInvalido()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 2, -100.00m);
            transacao.SetStatus(EStatusTransacao.ValorTransacaoInvalido);
            TestarSaidaConsole(transacao, $"Transacao numero {transacao.Id} nao foi efetivada pois o valor de transacao e invalido");
        }

        private void TestarSaidaConsole(Transacao transacao, string expectedOutput)
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            ImprimirResultadoTransacoesService.Imprimir(transacao);

            string consoleOutput = sw.ToString().Trim();

            consoleOutput.Should().Be(expectedOutput);
        }
    }
}
