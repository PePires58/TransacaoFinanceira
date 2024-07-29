using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransacaoFinanceira.Aplicacao.Servicos;
using TransacaoFinanceira.DB.Contexto;
using TransacaoFinanceira.DB.Repositorios;
using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Exceptions;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Enums;

namespace TransacaoFinanceira.Tests
{
    [TestClass]
    public class VerificaTransacaoServiceTests
    {
        DbContextMock DbContext { get; }
        IRepositorioContas RepositorioContas { get; }
        IVerificaTransacaoService VerificaTransacaoService { get; }

        public VerificaTransacaoServiceTests()
        {
            DbContext = new();
            RepositorioContas = new RepositorioContas(DbContext);
            VerificaTransacaoService = new VerificaTransacaoService(RepositorioContas);
        }


        [TestMethod]
        public void DeveEstourarErroDeContaOrigemNaoEncontrada()
        {
            Transacao transacao = new(1, DateTime.Now, 1, 1, 0);
            VerificaTransacaoService.VerificarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.ContaOrigemNaoExiste);
        }

        [TestMethod]
        public void DeveEstourarErroDeContaDestinoNaoEncontrada()
        {
            Transacao transacao = new(1, DateTime.Now, DbContext.ContasDb.First().Key, 1, 0);
            VerificaTransacaoService.VerificarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.ContaDestinoNaoExiste);
        }

        [TestMethod]
        public void DeveEstourarErroPoisContasSaoIguais()
        {
            Transacao transacao = new(1, DateTime.Now, DbContext.ContasDb.First().Key, DbContext.ContasDb.First().Key, 0);
            VerificaTransacaoService.VerificarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.ContasIguais);
        }

        [TestMethod]
        public void DeveEstourarErroPoisTransacaoEstaComValorInvalido()
        {
            Transacao transacao = new(1, DateTime.Now, DbContext.ContasDb.First().Key, DbContext.ContasDb.Last().Key, 0);
            VerificaTransacaoService.VerificarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.ValorTransacaoInvalido);
        }

        [TestMethod]
        public void NaoDeveEstourarErros()
        {
            Transacao transacao = new(1, DateTime.Now,
                DbContext.ContasDb.First().Key, DbContext.ContasDb.Last().Key, 10);

            VerificaTransacaoService.VerificarTransacao(transacao);

            transacao.Status.Should().Be(EStatusTransacao.AguardandoProcessamento);
        }

    }
}
