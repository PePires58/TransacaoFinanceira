using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Handlers;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Entidades;
using TransacaoFinanceira.Enums;

namespace TransacaoFinanceira.Servicos
{
    public class TransacoesHandler : ITransacoesHandler
    {
        IRepositorioContas Repositorio { get; }
        IVerificaTransacaoService VerificarExistenciaContasService { get; }
        ITransacaoService TransacaoService { get; }

        public TransacoesHandler(IRepositorioContas repositorio, IVerificaTransacaoService verificarExistenciaContasService,
            ITransacaoService transacaoService)
        {
            Repositorio = repositorio;
            TransacaoService = transacaoService;
            VerificarExistenciaContasService = verificarExistenciaContasService;
        }

        public void RealizarTransacoes(Transacao[] transacoes)
        {
            transacoes = [.. transacoes.OrderBy(c => c.DataHora)];

            foreach (var item in transacoes)
            {
                RealizarTransacao(item);
            }
        }

        private void RealizarTransacao(Transacao transacao)
        {

            VerificarExistenciaContasService.VerificarTransacao(transacao);

            if (transacao.Status != EStatusTransacao.AguardandoProcessamento)
                return;

            Conta contaOrigem = Repositorio.Consultar(transacao.IdContaOrigem);
            Conta contaDestino = Repositorio.Consultar(transacao.IdContaDestino);
            transacao.SetContasTransacao(contaOrigem, contaDestino);

            do
            {
                TransacaoService.EfetivarTransacao(transacao);

            } while (transacao.Status != EStatusTransacao.Processada &&
                transacao.Status != EStatusTransacao.SaldoInsuficiente);
        }
    }
}
