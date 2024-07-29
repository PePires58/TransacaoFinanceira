using TransacaoFinanceira.Dominio.Entidades;
using TransacaoFinanceira.Dominio.Repositorios;
using TransacaoFinanceira.Dominio.Servicos;
using TransacaoFinanceira.Enums;

namespace TransacaoFinanceira.Aplicacao.Servicos
{
    public class TransacaoService(IRepositorioContas Repositorio) : ITransacaoService
    {
        public void EfetivarTransacao(Transacao transacao)
        {
            if (transacao.ContaOrigem.Saldo >= transacao.ValorTransacao)
            {
                transacao.SetStatus(EStatusTransacao.EmProcessamento);

                transacao.ContaOrigem.Saldo -= transacao.ValorTransacao;
                transacao.ContaDestino.Saldo += transacao.ValorTransacao;

                Repositorio.Atualizar(transacao.ContaOrigem);
                Repositorio.Atualizar(transacao.ContaDestino);

                transacao.SetStatus(EStatusTransacao.Processada);
            }
            else
                transacao.SetStatus(EStatusTransacao.SaldoInsuficiente);
        }
    }
}
